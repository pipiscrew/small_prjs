using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace north
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "SearchApi",
               routeTemplate: "api/{controller}/{action}/{_search}/{nd}/{rows}/{page}/{sidx}/{sord}/{filters}",
               defaults: new { filters = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "SelectSkipTakeApi",
               routeTemplate: "api/{controller}/{action}/{sidx}/{sord}/{page}/{rows}"
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}
