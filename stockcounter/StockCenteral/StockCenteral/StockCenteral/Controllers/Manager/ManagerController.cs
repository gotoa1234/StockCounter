using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockCenteral.Controllers.Manager
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 當管理者按下編輯時 (未送出資料屬於查詢狀態所以用get)
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult KendoManagerEditForm(string GUID)
        {
            var _Service = new Service.Service.Manager();
            var temp = _Service.Get_rowdata(GUID);
            return View(temp);//return rowdata

        }

        /// <summary>
        /// 當管理者按下新增時
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult KendoManagerAddForm(string GUID)
        {
            return View();

        }

        /// <summary>
        /// 編輯單一一筆資料
        /// </summary>
        /// <param name="EditData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult KendoManagerEditForm_Edit(Model.ViewModel.NewsBoard.NewsBoardViewModel EditData)
        {
            var _Service = new Service.Service.Manager();
            TempData["Result"] = _Service.Edit_rowdata(EditData);
           return RedirectToAction("KendoManagerEditForm" , "Manager", EditData );
        }



        /// <summary>
        /// 新增一筆資料
        /// </summary>
        /// <param name="AddData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult KendoManagerEditForm_Add(Model.ViewModel.NewsBoard.NewsBoardViewModel AddData)
        {
            var _Service = new Service.Service.Manager();
            TempData["Result"] = _Service.Add_rowdata(AddData);
            return RedirectToAction("KendoManagerAddForm", "Manager", AddData);
        }

    }
}