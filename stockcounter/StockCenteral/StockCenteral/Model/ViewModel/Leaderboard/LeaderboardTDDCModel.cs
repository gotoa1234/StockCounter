using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.Leardboard
{
    public class LeaderboardTDDCModel
    {

        public string Table_state { get; set; }
        //股票對應表資料名稱
        public string mapping_tablename{ get; set; } 

        //上升 六周計算 
        public double RetailRate { get; set; }

        /// <summary>
        /// 各期散戶資料(50以下)
        /// </summary>
        public string RetailEachOfData { get; set; }
        //下降 六周計算
        public double LaregeRate { get; set; }

        /// <summary>
        /// 各期大戶資料(800以上)
        /// </summary>
        public string LaregeEachOfData { get; set; }

        /// <summary>
        /// 大戶資料(只有1000以上)
        /// </summary>
        public double LaregeOnlyThousandRate { get; set; }

    }
}
