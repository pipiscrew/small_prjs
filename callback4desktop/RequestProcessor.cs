using System.IO;
using System.Net;

namespace WindowsFormsApplication37
{
    internal class RequestProcessor : IHttpRequestProcessor
    {
        public RequestProcessor()
        {
        }

        public void Process(HttpListenerContext ctx)
        {
            if (ctx == null || ctx.Request == null || ctx.Response == null)
            {
                return;
            }

            if (!ctx.Request.RemoteEndPoint.Address.Equals(IPAddress.Loopback))
            {
                return;
            }

            ctx.Response.StatusCode = 200;

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Hi! from the app");
            Stream outputStream = ctx.Response.OutputStream;
            outputStream.Write(buffer, 0, buffer.Length);


            ctx.Response.OutputStream.Close();
        }




    }
}
