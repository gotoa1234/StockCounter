using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.QueryModel
{
    public class SingleStockQueryExtentionKindChartParam
    {
        /// <summary>
        /// [TDDC_Mapping] 資料表中的TableName
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 搜尋的起始時間
        /// </summary>
        public DateTime Date_Start { get; set; }
        /// <summary>
        /// 搜尋的結尾時間
        /// </summary>
        public DateTime Date_End { get; set; }

        public List<List<bool>> Combination { get; set; } 

        public List<string> GroupName { get; set; }

        public int GroupCount { get; set; }
    }
}
