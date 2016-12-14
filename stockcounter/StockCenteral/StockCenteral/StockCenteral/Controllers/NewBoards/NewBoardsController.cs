using Model.ViewModel.NewsBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockCenteral.Controllers.NewBoards
{
    public class NewBoardsController : Controller
    {
        // GET: NewBoards
        public ActionResult Index()
        {
            var _Service = new Service.Service.Manager();
            List<NewsBoardViewModel> Result = _Service.Get_Newboards();

            return View(Result.OrderByDescending(o => o.Datetime).ToList());
        }

        [HttpPost]
        public ActionResult Index(NewsBoardViewModel model)
        {

            return View();
        }
    }
}