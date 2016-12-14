using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryStock.OriClass
{
    /// <summary>
    /// 集保戶股權資料的基本資料
    /// </summary>
    public class Counter
    {
        /// <summary>
        /// 級距序號
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 級距
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 人數
        /// </summary>
        public int People { get; set; }
        /// <summary>
        /// 股數
        /// </summary>
        public int PerShare { get; set; }

        /// <summary>
        /// 佔集保庫存數比例 (%)
        /// </summary>
        public double CHEP { get; set; }

        /// <summary>
        /// 股票代號
        /// </summary>
        public string StockNo { get; set; }

        /// <summary>
        /// 股票中文名稱
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }

    }
}
