using System;
using System.Net;

namespace WindowsFormsApplication37
{
    public interface IHttpRequestProcessor
    {
        void Process(HttpListenerContext ctx);
    }
}
