using Model.QueryModel;
using Model.ViewModel.SingleStock;
using StockCenteral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockCenteral.Controllers.SingleStock
{
    public class SingleStockApiController : ApiController
    {
        /// <summary>
        /// 取得股票比例資訊表資料
        /// </summary>
        [HttpPost]
        public List<SingleStockQueryCHEPResult> GetSingleStockDB(SingleStockQueryParam QueryParam)
        {
            var _Service = new Service.Service.SingalStock();
            var Result = _Service.QueryData(QueryParam);
             return Result;
        }

        /// <summary>
        /// 取得股東人數數量表資料
        /// </summary>
        [HttpPost]
        public List<SingleStockQueryCHEPResult> GetSingleStockPeopleDB(SingleStockQueryParam QueryParam)
        {
            var _Service = new Service.Service.SingalStock();
            var Result = _Service.QueryPeopleData(QueryParam);
            return Result;
        }

        /// <summary>
        /// 取出該股各級資料 level 1 ~15  - 比率
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public SingleStockChartQueryModel GetSingleStockChart(SingleStockQueryParam QueryParam)
        {
            SingleStockChartQueryModel result = new SingleStockChartQueryModel();
            result.xAxis = new List<string>();
            result.seriesLevel1 = new List<double>();
            result.seriesLevel2 = new List<double>();
            result.seriesLevel3 = new List<double>();
            result.seriesLevel4 = new List<double>();
            result.seriesLevel5 = new List<double>();
            result.seriesLevel6 = new List<double>();
            result.seriesLevel7 = new List<double>();
            result.seriesLevel8 = new List<double>();
            result.seriesLevel9 = new List<double>();
            result.seriesLevel10 = new List<double>();
            result.seriesLevel11 = new List<double>();
            result.seriesLevel12 = new List<double>();
            result.seriesLevel13 = new List<double>();
            result.seriesLevel14 = new List<double>();
            result.seriesLevel15 = new List<double>();
            var _Service = new Service.Service.SingalStock();
            var Get = _Service.QueryData(QueryParam);

            #region 資料分類整理
            for (int i = 0; i < Get.Count(); i++)
            {
                result.xAxis.Add(Get[i].ShowDate);
                result.seriesLevel1.Add(Get[i].Level_1);
                result.seriesLevel2.Add(Get[i].Level_2);
                result.seriesLevel3.Add(Get[i].Level_3);
                result.seriesLevel4.Add(Get[i].Level_4);
                result.seriesLevel5.Add(Get[i].Level_5);
                result.seriesLevel6.Add(Get[i].Level_6);
                result.seriesLevel7.Add(Get[i].Level_7);
                result.seriesLevel8.Add(Get[i].Level_8);
                result.seriesLevel9.Add(Get[i].Level_9);
                result.seriesLevel10.Add(Get[i].Level_10);
                result.seriesLevel11.Add(Get[i].Level_11);
                result.seriesLevel12.Add(Get[i].Level_12);
                result.seriesLevel13.Add(Get[i].Level_13);
                result.seriesLevel14.Add(Get[i].Level_14);
                result.seriesLevel15.Add(Get[i].Level_15);
            }
            #endregion
            
            
            return result;
        }

        /// <summary>
        /// 取出該股分群資料 50以下 50~800 800以上 股票比例
        /// </summary>
        /// <param name="QueryParam"></param>
        /// <returns></returns>
        [HttpPost]
        public SingleStockKindChartQueryModel GetSingleStockKindChart(SingleStockQueryParam QueryParam)
        {
            SingleStockKindChartQueryModel result = new SingleStockKindChartQueryModel();
            result.xAxis = new List<string>();
            result.Retail = new List<double>();
            result.Solid_household = new List<double>();
            result.Large = new List<double>();

            var _Service = new Service.Service.SingalStock();
            var Get = _Service.QueryData(QueryParam);

            #region 資料分類整理
            for (int i = 0; i < Get.Count(); i++)
            {
                result.xAxis.Add(Get[i].ShowDate);
                result.Retail.Add(Get[i].Level_1 + Get[i].Level_2 + Get[i].Level_3 + Get[i].Level_4 + Get[i].Level_5 + Get[i].Level_6 + Get[i].Level_7 + Get[i].Level_8);
                result.Solid_household.Add(Get[i].Level_9 + Get[i].Level_10 + Get[i].Level_11+ Get[i].Level_12 + Get[i].Level_13);
                result.Large.Add( Get[i].Level_14 + Get[i].Level_15);
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 取出股票代號對應的中文名稱
        /// </summary>
        /// <param name="QueryParam"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetSingleStockMippingName(SingleStockQueryParam QueryParam)
        {
            var _Service = new Service.Service.SingalStock();
            return _Service.MappingName(QueryParam);
        }


        /// <summary>
        /// 取出該股使用者自訂的分類資料
        /// </summary>
        /// <param name="QueryParam"></param>
        /// <returns></returns>
        [HttpPost]
        public SingleStockExtentionKindChartQueryModel GetSingleStockExtentionKindChart(SingleStockQueryExtentionKindChartParam QueryParam)
        {
            SingleStockExtentionKindChartQueryModel result = new SingleStockExtentionKindChartQueryModel();
            result.xAxis = new List<string>();
            result.GroupName = new List<string>();
            result.Detail = new List<List<double>>();

            var _Service = new Service.Service.SingalStock();
            var Get = _Service.QueryDataForExtention(QueryParam);

            #region 資料分類整理 -依照使用者的選擇來組群組


            for (int sq = 0; sq < QueryParam.GroupCount; sq++)
            {
                result.GroupName.Add(QueryParam.GroupName[sq]);
                //群組的選擇bool
                List<bool> GroupSelect = new List<bool>();
                for (int i = 0; i < 15; i++)
                {
                    GroupSelect.Add(QueryParam.Combination[sq][i]);
                }


                List<double> tempdetail = new List<double>();
                double Sum = 0;
                for (int i = 0; i < Get.Count(); i++)
                {
                    Sum = 0;
                    for (int k=0;k <15;k++)
                    {
                        if (GroupSelect[k])
                        {
                            if(k==0)
                                 Sum += Get[i].Level_1;
                            else if(k==1)
                                Sum += Get[i].Level_2;
                            else if (k == 2)
                                Sum += Get[i].Level_3;
                            else if (k == 3)
                                Sum += Get[i].Level_4;
                            else if (k == 4)
                                Sum += Get[i].Level_5;
                            else if (k == 5)
                                Sum += Get[i].Level_6;
                            else if (k == 6)
                                Sum += Get[i].Level_7;
                            else if (k == 7)
                                Sum += Get[i].Level_8;
                            else if (k == 8)
                                Sum += Get[i].Level_9;
                            else if (k == 9)
                                Sum += Get[i].Level_10;
                            else if (k == 10)
                                Sum += Get[i].Level_11;
                            else if (k == 11)
                                Sum += Get[i].Level_12;
                            else if (k == 12)
                                Sum += Get[i].Level_13;
                            else if (k == 13)
                                Sum += Get[i].Level_14;
                            else if (k == 14)
                                Sum += Get[i].Level_15;
                        }
                    }
                    tempdetail.Add(Sum);
                }
                result.Detail.Add(tempdetail);
            }

            for (int i = 0; i < Get.Count(); i++)
            {
                result.xAxis.Add(Get[i].ShowDate);
            }
                #endregion

            return result;
        }

        /// <summary>
        /// 取出該股股東人數各級資料 level 1 ~15  - 人數
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public SingleStockChartQueryModel GetSingleStockPeopleChart(SingleStockQueryParam QueryParam)
        {
            SingleStockChartQueryModel result = new SingleStockChartQueryModel();
            result.xAxis = new List<string>();
            result.seriesLevel1 = new List<double>();
            result.seriesLevel2 = new List<double>();
            result.seriesLevel3 = new List<double>();
            result.seriesLevel4 = new List<double>();
            result.seriesLevel5 = new List<double>();
            result.seriesLevel6 = new List<double>();
            result.seriesLevel7 = new List<double>();
            result.seriesLevel8 = new List<double>();
            result.seriesLevel9 = new List<double>();
            result.seriesLevel10 = new List<double>();
            result.seriesLevel11 = new List<double>();
            result.seriesLevel12 = new List<double>();
            result.seriesLevel13 = new List<double>();
            result.seriesLevel14 = new List<double>();
            result.seriesLevel15 = new List<double>();
            var _Service = new Service.Service.SingalStock();
            var Get = _Service.QueryPeopleData(QueryParam);

            #region 資料分類整理
            for (int i = 0; i < Get.Count(); i++)
            {
                result.xAxis.Add(Get[i].ShowDate);
                result.seriesLevel1.Add(Get[i].Level_1);
                result.seriesLevel2.Add(Get[i].Level_2);
                result.seriesLevel3.Add(Get[i].Level_3);
                result.seriesLevel4.Add(Get[i].Level_4);
                result.seriesLevel5.Add(Get[i].Level_5);
                result.seriesLevel6.Add(Get[i].Level_6);
                result.seriesLevel7.Add(Get[i].Level_7);
                result.seriesLevel8.Add(Get[i].Level_8);
                result.seriesLevel9.Add(Get[i].Level_9);
                result.seriesLevel10.Add(Get[i].Level_10);
                result.seriesLevel11.Add(Get[i].Level_11);
                result.seriesLevel12.Add(Get[i].Level_12);
                result.seriesLevel13.Add(Get[i].Level_13);
                result.seriesLevel14.Add(Get[i].Level_14);
                result.seriesLevel15.Add(Get[i].Level_15);
            }
            #endregion


            return result;
        }

        /// <summary>
        /// 取出該股分群資料 50以下 50~800 800以上 股東人數數量
        /// </summary>
        /// <param name="QueryParam"></param>
        /// <returns></returns>
        [HttpPost]
        public SingleStockKindChartQueryModel GetSingleStockPeopleKindChart(SingleStockQueryParam QueryParam)
        {
            SingleStockKindChartQueryModel result = new SingleStockKindChartQueryModel();
            result.xAxis = new List<string>();
            result.Retail = new List<double>();
            result.Solid_household = new List<double>();
            result.Large = new List<double>();

            var _Service = new Service.Service.SingalStock();
            var Get = _Service.QueryPeopleData(QueryParam);

            #region 資料分類整理
            for (int i = 0; i < Get.Count(); i++)
            {
                result.xAxis.Add(Get[i].ShowDate);
                result.Retail.Add(Get[i].Level_1 + Get[i].Level_2 + Get[i].Level_3 + Get[i].Level_4 + Get[i].Level_5 + Get[i].Level_6 + Get[i].Level_7 + Get[i].Level_8);
                result.Solid_household.Add(Get[i].Level_9 + Get[i].Level_10 + Get[i].Level_11 + Get[i].Level_12 + Get[i].Level_13);
                result.Large.Add(Get[i].Level_14 + Get[i].Level_15);
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 取出該股使用者自訂的分類資料
        /// </summary>
        /// <param name="QueryParam"></param>
        /// <returns></returns>
        [HttpPost]
        public SingleStockExtentionKindChartQueryModel GetSingleStockPeopleExtentionKindChart(SingleStockQueryExtentionKindChartParam QueryParam)
        {
            SingleStockExtentionKindChartQueryModel result = new SingleStockExtentionKindChartQueryModel();
            result.xAxis = new List<string>();
            result.GroupName = new List<string>();
            result.Detail = new List<List<double>>();

            var _Service = new Service.Service.SingalStock();
            var Get = _Service.QueryDataForPeopleExtention(QueryParam);

            #region 資料分類整理 -依照使用者的選擇來組群組


            for (int sq = 0; sq < QueryParam.GroupCount; sq++)
            {
                result.GroupName.Add(QueryParam.GroupName[sq]);
                //群組的選擇bool
                List<bool> GroupSelect = new List<bool>();
                for (int i = 0; i < 15; i++)
                {
                    GroupSelect.Add(QueryParam.Combination[sq][i]);
                }


                List<double> tempdetail = new List<double>();
                double Sum = 0;
                for (int i = 0; i < Get.Count(); i++)
                {
                    Sum = 0;
                    for (int k = 0; k < 15; k++)
                    {
                        if (GroupSelect[k])
                        {
                            if (k == 0)
                                Sum += Get[i].Level_1;
                            else if (k == 1)
                                Sum += Get[i].Level_2;
                            else if (k == 2)
                                Sum += Get[i].Level_3;
                            else if (k == 3)
                                Sum += Get[i].Level_4;
                            else if (k == 4)
                                Sum += Get[i].Level_5;
                            else if (k == 5)
                                Sum += Get[i].Level_6;
                            else if (k == 6)
                                Sum += Get[i].Level_7;
                            else if (k == 7)
                                Sum += Get[i].Level_8;
                            else if (k == 8)
                                Sum += Get[i].Level_9;
                            else if (k == 9)
                                Sum += Get[i].Level_10;
                            else if (k == 10)
                                Sum += Get[i].Level_11;
                            else if (k == 11)
                                Sum += Get[i].Level_12;
                            else if (k == 12)
                                Sum += Get[i].Level_13;
                            else if (k == 13)
                                Sum += Get[i].Level_14;
                            else if (k == 14)
                                Sum += Get[i].Level_15;
                        }
                    }
                    tempdetail.Add(Sum);
                }
                result.Detail.Add(tempdetail);
            }

            for (int i = 0; i < Get.Count(); i++)
            {
                result.xAxis.Add(Get[i].ShowDate);
            }
            #endregion

            return result;
        }

    }
}
