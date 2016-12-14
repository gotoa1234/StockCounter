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
        public Model.ModelDB.TDDC_Mapping MappingOption { get; set; }

        /// <summary>
        /// 回傳的Entitiy 資料表
        /// </summary>
        public List<EntityType>  TDDC {get;set;}
    }
}