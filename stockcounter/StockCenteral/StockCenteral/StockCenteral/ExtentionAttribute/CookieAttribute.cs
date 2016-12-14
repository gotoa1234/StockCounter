using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockCenteral.ExtentionAttribute
{
    public sealed class CookieAttribute : System.Web.Mvc.ActionFilterAttribute
    {
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //Get from browser
                filterContext.Controller.TempData["Account"] = filterContext.HttpContext.Request.Cookies["Account"].Value;
                filterContext.Controller.TempData["UserName"] = filterContext.HttpContext.Request.Cookies["UserName"].Value;
            }
            catch (Exception ex)
            {
                filterContext.Controller.TempData["ExMessage"] = ex.Message;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
       
        }
        /// <summary>
        /// 當Controller 離開時將Cookie塞入
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            try
            {

                //Set to browser
                filterContext.HttpContext.Response.Cookies["Account"].Value = (string)filterContext.Controller.TempData["Account"];
                filterContext.HttpContext.Response.Cookies["UserName"].Value = (string)filterContext.Controller.TempData["UserName"];
            }
            catch (Exception ex)
            {
                filterContext.Controller.TempData["ExMessage"] = ex.Message;
            }
        }

    }
}