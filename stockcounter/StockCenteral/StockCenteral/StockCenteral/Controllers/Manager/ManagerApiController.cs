using Model.ViewModel.NewsBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockCenteral.Controllers.Manager
{
    public class ManagerApiController : ApiController
    {
        /// <summary>
        /// 取得留言板內容
        /// </summary>
        [HttpGet]
        public List<NewsBoardViewModel> GetManagerDB()
        {
            var _Service = new Service.Service.Manager();
            var Result = _Service.Get_Newboards();
            return Result;
        }

        /// <summary>
        /// 移除該筆資料內容
        /// </summary>
        [HttpPost]
        public string Get_DeleteData(Model.ViewModel.NewsBoard.NewsBoardViewModel AddData)
        {
            var _Service = new Service.Service.Manager();
            var Result = _Service.Delete_rowdata(AddData.Guid.ToString());
            return Result;
        }
    }
}
