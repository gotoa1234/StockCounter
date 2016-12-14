using Service.Service;
using StockCenteral.ViewModel.SingleStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockCenteral.Controllers.SingleStock
{
  [Authorize]
    public class SingleStockController : Controller
    {
        private Service.Service.SingalStock _Service;//----預設帶入的Service功能
        // GET: SingleStock
        
        public ActionResult Index()
        {
            _Service = new Service.Service.SingalStock();
            var temp = _Service.InitinalData();
            
            return View(temp);
        }

        // 回傳AJAX Stock No 代號表
        [HttpPost]
        public ActionResult AddSelectStockNo()
        {
            _Service = new Service.Service.SingalStock();
            var temp = _Service.InitinalData();

            return View(temp);
        }

        /// <summary>
        /// 籌碼圖表
        /// </summary>
        /// <returns></returns>
        public ActionResult SingleStockChart()
        {
            return View();
        }
    }
}