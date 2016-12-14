using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.SingleStock
{
    /// <summary>
    /// 當使用者查詢後 統一使用的model表格 對應datatable plugin
    /// </summary>
    public partial class SingleStockQueryModel
    {
            public int Level { get; set; }
            public string Class { get; set; }
            public int People { get; set; }
            public int PerShare { get; set; }
            public double CHEP { get; set; }
            public string StockNo { get; set; }
            public string StockName { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
       

    }
}
