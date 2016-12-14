using Model.QueryModel;
using Model.ViewModel.SingleStock;
using StockCenteral.ViewModel.SingleStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{

    public interface ISingalStock
    {
        SingleStockModel InitinalData();//---登入時的基本資料回傳

        List<SingleStockQueryCHEPResult> QueryData(SingleStockQueryParam Query);//------查詢時的股票比例資料回傳

        List<SingleStockQueryCHEPResult> QueryPeopleData(SingleStockQueryParam Query);//查詢時的股東人數資料回傳

        List<SingleStockQueryCHEPResult> QueryDataForExtention(SingleStockQueryExtentionKindChartParam Query);//----自訂股票比例回傳

        List<SingleStockQueryCHEPResult> QueryDataForPeopleExtention(SingleStockQueryExtentionKindChartParam Query);//自訂股東人數回傳

        string MappingName(SingleStockQueryParam Query);// 取出股票對應的中文名稱
    }
}
