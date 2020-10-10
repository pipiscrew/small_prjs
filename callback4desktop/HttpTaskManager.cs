using System;
using System.Net;

namespace WindowsFormsApplication37
{
    public class HttpTaskManager
    {
        public HttpTaskManager(Action<HttpListenerContext> req)
        {
            this.req = req;
        }

        public void Run(HttpListenerContext ctx)
        {
            new HttpTask(ctx, this.req).Start();
        }

        private Action<HttpListenerContext> req;
    }
}
