using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace StockCenteral.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional },
             constraints: new { id = @"^[0-9]+$" }
                 );

            config.Routes.MapHttpRoute(
             name: "ApiByName",
             routeTemplate: "api/{controller}/{action}/{name}",
             defaults: null,
             constraints: new { name = @"^[a-z]+$" }
         );
            config.Routes.MapHttpRoute(
            name: "ApiByAction",
            routeTemplate: "api/{controller}/{action}",
            defaults: new { action = "Get" }
        );
            //NuGet： Microsoft.AspNet.WebApi.Cors
            config.EnableCors();
        }
    }
}