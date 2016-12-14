using Model.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockCenteral.Controllers.Register
{
    public class RegisterController : Controller
    {
        // GET: Register
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // Post: Register/Input
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Input(RegisterViewModel UserInput)
        {
            var _Service = new Service.Service.Register();
            if (!_Service.AccountRepeatJudge(UserInput))
            {
                _Service.Add(UserInput);

                return RedirectToAction("Index", "Home");//----------註冊成功導向到首頁
            }
            else
            {
                TempData["ErrorMessage"] = "註冊帳號資訊重複";
            }

            return RedirectToAction("Index", "Register");


        }
    }
}