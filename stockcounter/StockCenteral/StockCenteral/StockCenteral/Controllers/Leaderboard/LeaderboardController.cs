using Model.QueryModel;
using Model.ViewModel.Leardboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockCenteral.Controllers.Leaderboard
{

    public class LeaderboardController : Controller
    {

        // GET: Leaderboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LeaderboardResultTDDCModel Data)
        {
            LeaderboardTDDCModel data = new LeaderboardTDDCModel();
            var _Service = new Service.Service.Leardboard();

            //Query combine
            LeaderboardTDDCQueryParam QueryParam = new LeaderboardTDDCQueryParam();
            QueryParam.Date_End = Data.QueryInfom.Date_End;
            QueryParam.Date_Start = Data.QueryInfom.Date_Start;

            //Query searchresult
            LeaderboardResultTDDCModel Result = new LeaderboardResultTDDCModel();
            Result.QueryInfom = new LeaderboardTDDCQueryParam();
            Result.QueryResult = new List<LeaderboardTDDCModel>();
            Result.QueryInfom.Date_Start = Data.QueryInfom.Date_Start;
            Result.QueryInfom.Date_End = Data.QueryInfom.Date_End;
            Result.QueryResult = _Service.QueryData(QueryParam);

            //返回不重複的資料項目
            var tempDis = Result.QueryResult.Select(o=>o.mapping_tablename).Distinct().ToList();
            LeaderboardResultTDDCModel Result2 = new LeaderboardResultTDDCModel();
            Result2.QueryResult = new List<LeaderboardTDDCModel>();
            foreach (var every in tempDis)
            {
                Result2.QueryResult.Add(Result.QueryResult.Where(o => o.mapping_tablename == every).FirstOrDefault());
            }
            Result.QueryResult = Result2.QueryResult;
           
            return View(Result);
        }
        /// <summary>
        /// 將查詢結果傳遞到子頁面中
        /// </summary>
        /// <param name="QueryParam"></param>
        /// <returns></returns>
        public ActionResult LeaderBoardSparkline(LeaderboardTDDCQueryParam QueryParam)
        {
            var _Service = new Service.Service.Leardboard();
            var result = _Service.QueryData(QueryParam);
            //返回不重複的資料項目
            var tempDis = result.Select(o => o.mapping_tablename).Distinct().ToList();
            List<LeaderboardTDDCModel> Result2 = new List<LeaderboardTDDCModel>();
            foreach (var every in tempDis)
            {
                Result2.Add(result.Where(o => o.mapping_tablename == every).FirstOrDefault());
            }
            return PartialView("LeaderBoardSparkline" , Result2);
        }
    }
}