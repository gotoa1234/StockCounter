using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace StockCenteral.ViewModel.SingleStock
{
    public class SingleStockModel
    {
        /// <summary>
        /// 可搜尋的資料對應表
        /// </summary>
        public System.Data.Entity.DbSet<Model.ModelDB.TDDC_Mapping> MappingOption { get; set; }

       
    }
}