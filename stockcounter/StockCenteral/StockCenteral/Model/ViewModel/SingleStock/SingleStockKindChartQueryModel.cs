using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.SingleStock
{
    public class SingleStockKindChartQueryModel
    {

        public List<string> xAxis { get; set; }
 
        /// <summary>
        /// 散戶 50以下
        /// </summary>
        public List<double> Retail { get; set; }
        /// <summary>
        /// 中實戶 50~ 800
        /// </summary>
        public List<double> Solid_household { get; set; }
        /// <summary>
        /// 大戶 800以上
        /// </summary>
        public List<double> Large { get; set; }
    }
}
