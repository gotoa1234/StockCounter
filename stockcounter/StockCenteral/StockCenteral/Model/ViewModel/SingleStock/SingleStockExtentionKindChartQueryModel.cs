using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.SingleStock
{
    public class SingleStockExtentionKindChartQueryModel
    {
        /// <summary>
        /// 群組名稱
        /// </summary>
        public List<string> GroupName { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public List<string> xAxis { get; set; }
 
        /// <summary>
        /// 群組資料 
        /// /// </summary>
        public List<List<double>> Detail { get; set; }

    }
}
