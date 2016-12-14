using StockCenteral.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StockCenteral
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            GlobalConfiguration.Configure(WebApiConfig.Register);//---啟動web api 設定新路由導向，使其可以讀取
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //my set
            GlobalFilters.Filters.Add(new System.Web.Mvc.AuthorizeAttribute());//
        }

        protected void Application_BeginRequest()
        {
            NLog.MappedDiagnosticsLogicalContext.Set("requestId",
            JSNLog.JavascriptLogging.RequestId());
        }
    }
}
