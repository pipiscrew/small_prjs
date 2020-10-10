using System;
using System.Net;
using System.Threading.Tasks;

namespace WindowsFormsApplication37
{
    internal class HttpTask : IEquatable<HttpTask>, IDisposable
    {
        public HttpTask(HttpListenerContext ctx, Action<HttpListenerContext> req)
        {
            this.url = ctx.Request.RawUrl;
            this.task = new Task(delegate
            {
                req(ctx);
            });
        }

        public void Dispose()
        {
            Task x = this.task;
            if (x == null)
            {
                return;
            }
            x.Dispose();
        }

        public bool Equals(HttpTask task)
        {
            return this.url.Equals(task.url);
        }

        public void Start()
        {
            this.task.Start();
        }

        public bool IsRunning
        {
            get
            {
                return this.task.Status == TaskStatus.Running || this.task.Status == TaskStatus.WaitingToRun;
            }
        }

        private Task task;

        private string url;
    }
}
