using Model.QueryModel;
using Model.ViewModel.Leardboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockCenteral.Controllers.Leaderboard
{
    public class LeaderboardApiController : ApiController
    {

        /// <summary>
        /// 取出該分群資料 50以下 50~400 400以上
        /// </summary>
        /// <param name="QueryParam"></param>
        /// <returns></returns>
        [HttpPost]
        public List<LeaderboardTDDCModel> GetLeaderboardTDDC(LeaderboardTDDCQueryParam QueryParam)
        {
            
            var _Service = new Service.Service.Leardboard();
            var result = _Service.QueryData(QueryParam);
            var tempDis = result.Select(o => o.mapping_tablename).Distinct();
            List<LeaderboardTDDCModel> Result2 = new List<LeaderboardTDDCModel>();
            foreach (var every in tempDis)
            {
                Result2.Add(result.Where(o => o.mapping_tablename == every).FirstOrDefault());
            }
            return Result2;
        }


    }
}
