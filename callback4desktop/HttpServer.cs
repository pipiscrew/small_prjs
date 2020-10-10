using System;
using System.Diagnostics;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication37
{
    public sealed class HttpServer : IDisposable
    {

        public static readonly string ADDRESS = string.Format("http://127.0.0.1:{0}/", "4565");

        private readonly HttpListener listener;

        private IHttpRequestProcessor reqProcessor;

        private HttpTaskManager taskManager;

        public HttpServer(IHttpRequestProcessor reqProcessor)
        {
            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");
            }
            this.reqProcessor = reqProcessor;
            this.listener = new HttpListener();
            this.listener.IgnoreWriteExceptions = true;
            this.listener.Prefixes.Add(HttpServer.ADDRESS);
            this.taskManager = new HttpTaskManager(new Action<HttpListenerContext>(this.dispatchRequest));
        }

        private void dispatchRequest(HttpListenerContext ctx)
        {
            if (ctx == null)
            {
                return;
            }
            try
            {
                this.reqProcessor.Process(ctx);
            }
            catch (HttpListenerException)
            {
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }

        public void Dispose()
        {
            this.listener.Stop();
            this.listener.Close();
        }


        public void Start()
        {
            if (this.listener.IsListening)
            {
                return;
            }
            this.listener.Start();
            Task.Factory.StartNew(delegate
            {
                while (this.listener.IsListening)
                {
                    HttpListenerContext context = this.listener.GetContext();
                    this.taskManager.Run(context);
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void Stop()
        {
            this.listener.Stop();
            this.listener.Close();
        }

        public bool IsListening
        {
            get
            {
                return this.listener.IsListening;
            }
        }

        public static void RegisterHttpServer (){
            
            //when prior Windows XP SP2
            // is totally different from the hosts file, 'netsh http' working on a much lower level of Windows
            //ref - https://superuser.com/questions/1272374/in-what-scenarios-will-i-use-netsh-http-add-urlacl

            string str = new SecurityIdentifier("S-1-1-0").Translate(typeof(NTAccount)).ToString();
            string[] textArray1 = new string[] { "http add urlacl url=", ADDRESS, " user=\"", str, "\"" };
            string arguments = string.Concat(textArray1);
            ProcessStartInfo startInfo = new ProcessStartInfo("netsh", arguments)
            {
                Verb = "runas",
                CreateNoWindow = true,
                UseShellExecute = true
            };
            Process.Start(startInfo).WaitForExit();

            /*
             * src - https://stackoverflow.com/a/59825373
                to show the urls :
                netsh http show urlacl

                to delete a url example :
                netsh http delete urlacl url=http://localhost:55521/
             */
        }

    }
}
