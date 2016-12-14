using Model.QueryModel;
using Model.ViewModel.Leardboard;
using Model.ViewModel.SingleStock;
using Service.IService;
using StockCenteral.ViewModel.SingleStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class Leardboard : ILeardboard
    {
        /// <summary>
        /// 取得時間排行榜內的籌碼趨勢比例排行
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<LeaderboardTDDCModel> QueryData(LeaderboardTDDCQueryParam Query)
        {
            //過濾搜尋時間 權重過濾法
            int StartDateSum = Query.Date_Start.Year * 1500 + Query.Date_Start.Month * 100 + Query.Date_Start.Day * 1;
            int EndDateSum = Query.Date_End.Year * 1500 + Query.Date_End.Month * 100 + Query.Date_End.Day * 1;

            using (var DB = new Model.ModelDB.BochenLinTestEntities())
            {
                //當前有紀錄的所有籌碼名稱
                var mappingtable = DB.TDDC_Mapping.ToList();
                double Retail_temp = 0;
                double Retail_temp_after = 0;
                double Large_temp = 0;
                double Large_temp_after = 0;
               
                //製作成輸出表
                List<LeaderboardTDDCModel> ResultSort = new List<LeaderboardTDDCModel>();
                LeaderboardTDDCModel SortSingle = new LeaderboardTDDCModel();

                List<SingleStockQueryCHEPResult> Result = new List<SingleStockQueryCHEPResult>();
                SingleStockQueryCHEPResult Single = new SingleStockQueryCHEPResult();
                foreach (var Now_TddcTable in mappingtable)
                {
                    if (!(Now_TddcTable.State == "上市" || Now_TddcTable.State == "上櫃" || Now_TddcTable.State == "興櫃"))
                        continue;

                    ////test
                    //if (ResultSort.Count() >= 200)
                    //{
                    //    break;
                    //}

                    Result = new List<SingleStockQueryCHEPResult>();
                    Single = new SingleStockQueryCHEPResult();

                    
                    //取得當前的資料表
                    var temp2 = DB.Database.SqlQuery<SingleStockQueryModel>(string.Format(string.Format("SELECT * FROM {0} Where (Year * 1500 + Month *100 + Day )<= {1} AND (Year * 1500 + Month *100 + Day ) >={2}", Now_TddcTable.TableName, EndDateSum, StartDateSum), 1));

                 
                    //排序資料列
                    var temp4 = temp2.OrderByDescending(o => o.Year).ThenByDescending(o => o.Month).ThenByDescending(o => o.Day).ThenBy(o => o.Level).ToList();
                    #region 資料收集
                    foreach (var tb in temp4)
                    {
                        //依照Level分類資料       
                        switch (tb.Level)
                        {
                            case 1:
                                Single.Level_1 = Math.Round(tb.CHEP, 2);
                                break;
                            case 2:
                                Single.Level_2 = Math.Round(tb.CHEP, 2);
                                break;
                            case 3:
                                Single.Level_3 = Math.Round(tb.CHEP, 2);
                                break;
                            case 4:
                                Single.Level_4 = Math.Round(tb.CHEP, 2);
                                break;
                            case 5:
                                Single.Level_5 = Math.Round(tb.CHEP, 2);
                                break;
                            case 6:
                                Single.Level_6 = Math.Round(tb.CHEP, 2);
                                break;
                            case 7:
                                Single.Level_7 = Math.Round(tb.CHEP, 2);
                                break;
                            case 8:
                                Single.Level_8 = Math.Round(tb.CHEP, 2);
                                break;
                            case 9:
                                Single.Level_9 = Math.Round(tb.CHEP, 2);
                                break;
                            case 10:
                                Single.Level_10 = Math.Round(tb.CHEP, 2);
                                break;
                            case 11:
                                Single.Level_11 = Math.Round(tb.CHEP, 2);
                                break;
                            case 12:
                                Single.Level_12 = Math.Round(tb.CHEP, 2);
                                break;
                            case 13:
                                Single.Level_13 = Math.Round(tb.CHEP, 2);
                                break;
                            case 14:
                                Single.Level_14 = Math.Round(tb.CHEP, 2);
                                break;
                            case 15:
                                Single.Level_15 = Math.Round(tb.CHEP, 2);
                                Single.ShowDate = tb.Year + "/" + tb.Month + "/" + tb.Day;
                                Single.Date = new DateTime(tb.Year, tb.Month, tb.Day);
                                Result.Add(Single);
                                Single = new SingleStockQueryCHEPResult();
                                break;
                        }
                    }
                    #endregion
                    //11/2 = 5 middle 6
                    #region 將時間區分為2 計算 前半段 與 後半段 時間的差異 (已經排序)
                    int middle = Result.Count() / 2;
                    Retail_temp = 0;
                    Retail_temp_after = 0;
                    Large_temp = 0;
                    Large_temp_after = 0;

                    //前半段資料

                    Retail_temp += Result.Take(middle).Sum(o => o.Level_1 + o.Level_2 + o.Level_3 + o.Level_4 + o.Level_5 + o.Level_6 + o.Level_7 + o.Level_8);
                    Large_temp += Result.Take(middle).Sum(o => o.Level_14 + o.Level_15);

                    //後半段資料
                    Retail_temp_after += Result.Skip(middle).Sum(o => o.Level_1 + o.Level_2 + o.Level_3 + o.Level_4 + o.Level_5 + o.Level_6 + o.Level_7 + o.Level_8);
                    Large_temp_after += Result.Skip(middle).Sum(o => o.Level_14 + o.Level_15);

                    
                    
                    

                    SortSingle = new LeaderboardTDDCModel();
                    SortSingle.LaregeEachOfData = "";
                    SortSingle.RetailEachOfData = "";
                    //每期資料 散戶
                    var tempRetail= Result.GroupBy(o=>o.ShowDate).Select(g=> new { EachOfTotal = g.Sum(o=>o.Level_1 + o.Level_2 + o.Level_3 + o.Level_4 + o.Level_5 + o.Level_6 + o.Level_7 + o.Level_8)  , ShowDate = g.Key});
                    var array =  tempRetail.Select(o => o.EachOfTotal).ToList();
                    foreach (var every in array)
                    {
                        if (array.IndexOf(every) != (array.Count()-1))
                            SortSingle.RetailEachOfData += every + " , ";
                        else
                            SortSingle.RetailEachOfData += every;
                    }
                    //每期資料 大戶
                    var tempLarege = Result.GroupBy(o => o.ShowDate).Select(g => new { EachOfTotal = g.Sum(o => o.Level_14 + o.Level_15), ShowDate = g.Key });
                    array = tempLarege.Select(o => o.EachOfTotal).ToList();
                    foreach (var every in array)
                    {
                        if (array.IndexOf(every) != (array.Count()-1))
                            SortSingle.LaregeEachOfData += every + " , ";
                        else
                            SortSingle.LaregeEachOfData += every;

                    }
                    //SortSingle.LaregeRateEachOfData.AddRange(tempLarege.Select(o => o.EachOfTotal).ToList());

                    SortSingle.mapping_tablename = Now_TddcTable.StockNo + " / " + Now_TddcTable.Ch;
                    SortSingle.RetailRate = Math.Round((Retail_temp / middle) - (Retail_temp_after / (Result.Count - middle)), 2);//散戶資料
                    SortSingle.LaregeRate = Math.Round((Large_temp / middle) - (Large_temp_after / (Result.Count - middle)), 2);//大戶資料
                    //只有千張大戶計算
                    double TLarge_temp= Result.Take(middle).Sum(o => o.Level_15);
                    double TLarge_after = Result.Skip(middle).Sum(o => o.Level_15);
                    SortSingle.LaregeOnlyThousandRate = Math.Round((TLarge_temp / middle) - (TLarge_after / (Result.Count - middle)), 2);//千戶資料

                    //狀態
                    SortSingle.Table_state = Now_TddcTable.State;
                    ResultSort.Add(SortSingle);
                    #endregion
                }


                //sort 散戶變小率
                return ResultSort.OrderBy(o => o.RetailRate).ToList();
            }
        }

    }
}
