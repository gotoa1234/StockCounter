using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace StockCenteral.Controllers.LogAttribute
{

    public sealed class Logattributes : System.Web.Mvc.ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //Server.MapPath("~/Content/test.zip")


            base.OnActionExecuting(filterContext);

            File.AppendAllText(HostingEnvironment.MapPath("~/App_Data") + "/ActionFilter.log",

               "【OnActionExecuting】執行時間：" +
                 DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);

        }


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Thread.Sleep(1000);
            File.AppendAllText(HostingEnvironment.MapPath("~/App_Data") + "/ActionFilter.log",
                "【OnActionExecuted】執行時間：" +
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Thread.Sleep(1000);
            File.AppendAllText(HostingEnvironment.MapPath("~/App_Data") + "/ActionFilter.log",
                "【OnResultExecuting】執行時間：" +
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Thread.Sleep(1000);
            File.AppendAllText(HostingEnvironment.MapPath("~/App_Data") + "/ActionFilter.log",
                "【OnResultExecuted】執行時間：" +
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
        }
    }
}