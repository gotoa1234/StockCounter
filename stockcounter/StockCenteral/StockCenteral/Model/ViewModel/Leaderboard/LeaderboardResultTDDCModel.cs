using Model.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.Leardboard
{
    public class LeaderboardResultTDDCModel
    {
        //頁面的查詢時間等資訊
        public LeaderboardTDDCQueryParam QueryInfom { get; set; }
        //股票搜尋結果類別表
        public List<LeaderboardTDDCModel> QueryResult { get; set; }


    }
}
