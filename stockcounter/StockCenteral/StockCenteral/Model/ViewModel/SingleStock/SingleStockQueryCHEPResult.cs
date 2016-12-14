using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.SingleStock
{
    /// <summary>
    /// 當使用者查詢後 並且組出 日期/Level 的比例排序表
    /// </summary>
    public class SingleStockQueryCHEPResult
    {
        /// <summary>
        /// 日期轉換格式 yyyy / mm /dd
        /// </summary>
        public string ShowDate { get; set; } 
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 0.001-0.999
        /// </summary>
        public double Level_1 { get; set; }
        /// <summary>
        /// 1~5
        /// </summary>
        public double Level_2 { get; set; }
        /// <summary>
        /// 5~10
        /// </summary>
        public double Level_3 { get; set; }
        /// <summary>
        /// 10~15
        /// </summary>
        public double Level_4 { get; set; }
        /// <summary>
        /// 15~20
        /// </summary>
        public double Level_5 { get; set; }
        /// <summary>
        /// 20~30
        /// </summary>
        public double Level_6 { get; set; }
        /// <summary>
        /// 30~40
        /// </summary>
        public double Level_7 { get; set; }
        /// <summary>
        /// 40~50
        /// </summary>
        public double Level_8 { get; set; }
        /// <summary>
        /// 50~100
        /// </summary>
        public double Level_9 { get; set; }
        /// <summary>
        /// 100~200
        /// </summary>
        public double Level_10 { get; set; }
        /// <summary>
        /// 200~400
        /// </summary>
        public double Level_11 { get; set; }
        /// <summary>
        /// 400~600
        /// </summary>
        public double Level_12 { get; set; }
        /// <summary>
        /// 600~800
        /// </summary>
        public double Level_13 { get; set; }
        /// <summary>
        /// 800~1000
        /// </summary>
        public double Level_14 { get; set; }
        /// <summary>
        /// 1000 over
        /// </summary>
        public double Level_15 { get; set; }

    }
}
