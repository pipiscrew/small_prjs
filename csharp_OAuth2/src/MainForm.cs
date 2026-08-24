using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

//specs - https://oauth.net/2/

namespace sampleOAuth2
{
    public partial class MainForm : Form
    {
        private const string ClientId = "7f3a9c1d2e8b4a6f"; //UID
        //private const string ClientSecret = "9Kx!4vQ#2mL@8pR$6tY%1zN"; //Secret

        // GET - request authorization
        private const string AuthorizeEndpoint =
            "https://example.com/oauth/authorize/";

        // POST - get authorization token
        private const string TokenEndpoint =
            "https://example.com/oauth/token";

        private const string RedirectUri =
            "https://127.0.0.1:49152/callback/";

        public MainForm()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                loginButton.Enabled = false;

                TokenResponse token = await AuthenticateAsync();

                MessageBox.Show(
                    "Login succeeded.\r\n\r\nAccess token:\r\n" +
                    token.AccessToken,
                    "OAuth",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Store token.AccessToken and token.RefreshToken securely.
                // Use token.AccessToken for API calls.
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "OAuth error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                loginButton.Enabled = true;
            }
        }

        private async Task<TokenResponse> AuthenticateAsync()
        {
            string state = CreateRandomString(32);

            // Start listening before opening the browser.
            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(RedirectUri);
                listener.Start();

                string authorizeUrl =
                    AuthorizeEndpoint +
                    "?client_id=" + Uri.EscapeDataString(ClientId) +
                    "&response_type=code" +
                    "&redirect_uri=" + Uri.EscapeDataString(RedirectUri) +
                    //"&scope=" + Uri.EscapeDataString("read") +
                    "&state=" + Uri.EscapeDataString(state);

                Process.Start(new ProcessStartInfo
                {
                    FileName = authorizeUrl,
                    UseShellExecute = true
                });

                // Wait for the browser to redirect back to the application.
                HttpListenerContext context =
                    await listener.GetContextAsync();

                string error = context.Request.QueryString["error"];
                if (!String.IsNullOrEmpty(error))
                {
                    SendBrowserResponse(
                        context,
                        "Authorization failed: " + error);

                    throw new InvalidOperationException(
                        "The user or authorization server rejected the request: " +
                        error);
                }

                string code = context.Request.QueryString["code"];
                string returnedState = context.Request.QueryString["state"];

                if (String.IsNullOrEmpty(code))
                {
                    SendBrowserResponse(
                        context,
                        "Authorization failed: no authorization code received.");

                    throw new InvalidOperationException(
                        "No authorization code was returned.");
                }

                if (!String.Equals(
                    state,
                    returnedState,
                    StringComparison.Ordinal))
                {
                    SendBrowserResponse(
                        context,
                        "Authorization failed: invalid state.");

                    throw new InvalidOperationException(
                        "The OAuth state value did not match.");
                }

                SendBrowserResponse(
                    context,
                    "Authorization completed. You may close this browser window.");

                // The browser step is complete. Now exchange the code for tokens.
                return await ExchangeCodeForTokenAsync(code);
            }
        }

        private async Task<TokenResponse> ExchangeCodeForTokenAsync(
     string authorizationCode)
        {
            using (HttpClient client = new HttpClient())
            {
                /*
                 * when #OAuth2 provider# setting is 'Confidential' :
                 * your application must send the #client_secret# (only when is WEB APPLICATION)
                 * protects the developer API resources from unauthorized apps aka global machine-to-machine trust.
				 * https://www.oauth.com/oauth2-servers/client-registration/client-id-secret/
				 *
                 * OTHERWISE (desktop, mobile) must send PKCE #code_challenge# and #code_challenge_method# params.
                 */

                string codeVerifier = PkceGenerator.CreateCodeVerifier();
                string codeChallenge = PkceGenerator.CreateCodeChallenge(codeVerifier);

                //Console.WriteLine("code_challenge_method: S256");
                //Console.WriteLine("code_verifier: " + codeVerifier);
                //Console.WriteLine("code_challenge: " + codeChallenge);
                //

                var values = new Dictionary<string, string>();

                values.Add("grant_type", "authorization_code");
                values.Add("client_id", ClientId);
                //values.Add("client_secret", ClientSecret);
                values.Add("code", authorizationCode);
                values.Add("redirect_uri", RedirectUri);
                values.Add("code_challenge", codeChallenge);
                values.Add("code_challenge_method", "S256");

                using (FormUrlEncodedContent content =
                    new FormUrlEncodedContent(values))
                {
                    HttpResponseMessage response =
                        await client.PostAsync(TokenEndpoint, content);

                    string responseText =
                        await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException(
                            "Token request failed: " +
                            response.StatusCode + "\r\n" +
                            responseText);
                    }

                    JavaScriptSerializer serializer =
                        new JavaScriptSerializer();

                    Dictionary<string, object> json =
                        serializer.Deserialize
                        <Dictionary<string, object>>(responseText);

                    TokenResponse result = new TokenResponse();

                    if (json.ContainsKey("access_token"))
                        result.AccessToken =
                            Convert.ToString(json["access_token"]);

                    if (json.ContainsKey("refresh_token"))
                        result.RefreshToken =
                            Convert.ToString(json["refresh_token"]);

                    if (json.ContainsKey("token_type"))
                        result.TokenType =
                            Convert.ToString(json["token_type"]);

                    if (json.ContainsKey("expires_in"))
                        result.ExpiresIn =
                            Convert.ToInt32(json["expires_in"]);

                    return result;
                }
            }
        }


        private static void SendBrowserResponse(
            HttpListenerContext context,
            string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(
                "<html><body><h2>" +
                WebUtility.HtmlEncode(message) +
                "</h2></body></html>");

            context.Response.ContentType = "text/html";
            context.Response.ContentLength64 = bytes.Length;
            context.Response.OutputStream.Write(
                bytes,
                0,
                bytes.Length);

            context.Response.OutputStream.Close();
        }

        private static string CreateRandomString(int byteCount)
        {
            byte[] bytes = new byte[byteCount];

            using (RandomNumberGenerator random =
                RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }

            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }
    }

}