using Microsoft.AspNet.Identity;
using Service.IService;
using Service.Service;
using StockCenteral.ExtentionAttribute;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;

namespace StockCenteral.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Account _Service;//----預設帶入的Service功能
        public HomeController()
        {
            _Service = new Service.Service.Account();
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]//頁面驗證時(submit)呼叫
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Model.ViewModel.Account.AccountViewModel Model)
        {

            //取得帳號資訊
            var temp = _Service.GetUserState(Model.Username, Model.Password);

            if (temp)//--false表示帳號密碼沒有對應到資料
            {
                #region Cookie build
                Response.Cookies["Account"].Value = Model.Username;
                Response.Cookies["UserName"].Value = _Service.UserInfomation(Model.Username).UserName;
                #endregion

                FormsAuthentication.SetAuthCookie(Model.Username, false);
                //有進行過驗證所以會導向到這個頁面
                return RedirectToAction("Index", "SingleStock");
            }
            ModelState.AddModelError(string.Empty, "帳號或密碼錯誤");
            return RedirectToAction("Index", "Home");//----------驗證失敗導回登入頁面
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            //清除Cookies 
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Account"].Expires = DateTime.Now.AddDays(-1);
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //清除.aspxauth
            FormsAuthentication.Initialize();
            string strRole = String.Empty;
            FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1, "", DateTime.Now, DateTime.Now.AddMinutes(-30), false, strRole, FormsAuthentication.FormsCookiePath);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));
            return RedirectToAction("Index", "Home");//----------回登入頁面
        }


        /// <summary>
        /// 取得登入驗證資訊
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [CookieAttribute]
        public ActionResult AuthAttribute()
        {
            return PartialView("_LoginPartial");//--資料回傳到_LoginPartial.cshtml中
        }



    }
}