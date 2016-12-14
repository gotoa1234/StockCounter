using Model.QueryModel;
using Model.ViewModel.SingleStock;
using Service.IService;
using StockCenteral.ViewModel.SingleStock;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class SingalStock : ISingalStock
    {
        //---登入時的基本資料回傳 - 提供股票資料表名稱
        public SingleStockModel InitinalData()
        {
            
            SingleStockModel MyData= new SingleStockModel();
            MyData.MappingOption =  new Model.ModelDB.BochenLinTestEntities().TDDC_Mapping;

            //刪除重複資料的程式
            bool flag = false;
            var tempt = MyData.MappingOption.ToList();
            var DB = new Model.ModelDB.BochenLinTestEntities();
            int count = 0;
            foreach (var temp in tempt)
            {
                count++;

                //if (temp.TableName == "TDDC_2316")
                //    flag = true;

                //if (temp.TableName == "TDDC_4438")
                //    break;//可以結束

                //if (flag == false)
                //    continue;

                //取得該資料表的所有資料
                //var tabledatarows = DB.Database.SqlQuery<SingleStockQueryModel>(string.Format("SELECT * FROM {0}", temp.TableName), 1);
                //我知道要刪除的日期
                var tabledatarows = DB.Database.SqlQuery<SingleStockQueryModel>(string.Format("SELECT * FROM {0} where year ={1} and month ={2} and day ={3} and level ={4}", temp.TableName ,"2016" ,"11" ,"11","15"), 1);


                var temp4 = tabledatarows.OrderByDescending(o => o.Year).ThenByDescending(o => o.Month).ThenByDescending(o => o.Day).ThenBy(o => o.Level).ToList();
                foreach (var onedata in temp4)
                {
                    //要刪除重複資料的數量
                    int deletecount = temp4.Where(o => o.CHEP == onedata.CHEP
                    && o.Class == onedata.Class
                    && o.Day == onedata.Day
                    && o.Level == onedata.Level
                    && o.Month == onedata.Month
                    && o.People == onedata.People
                    && o.PerShare == onedata.PerShare
                    && o.StockName == onedata.StockName
                    && o.StockNo == onedata.StockNo
                    && o.Year == onedata.Year
                    ).Count();
                    string table = temp.TableName;
                    if (deletecount > 1)
                    {
                        DB.Database.ExecuteSqlCommand(string.Format("Insert INTO {0} (CHEP , Class , Day , Level , Month , People , PerShare , StockName , StockNo , Year) VALUES (@CHEP , @Class , @Day , @Level , @Month , @People , @PerShare , @StockName , @StockNo , @Year)", temp.TableName)
                        ,
                         new SqlParameter("@CHEP", onedata.CHEP),
                         new SqlParameter("@Class", onedata.Class),
                         new SqlParameter("@Day", onedata.Day),
                         new SqlParameter("@Level", onedata.Level),
                         new SqlParameter("@Month", onedata.Month),
                         new SqlParameter("@People", onedata.People),
                         new SqlParameter("@PerShare", onedata.PerShare),
                         new SqlParameter("@StockName", onedata.StockName),
                         new SqlParameter("@StockNo", onedata.StockNo),
                          new SqlParameter("@Year", onedata.Year));

                        DB.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0} Where CHEP = @CHEP AND Class = @Class AND Day = @Day AND Level = @Level AND Month = @Month AND People = @People AND PerShare = @PerShare AND StockName = @StockName AND StockNo = @StockNo AND Year = @Year",temp.TableName)
                            ,
                            new SqlParameter("@CHEP", onedata.CHEP),
                            new SqlParameter("@Class", onedata.Class),
                            new SqlParameter("@Day", onedata.Day),
                            new SqlParameter("@Level", onedata.Level),
                            new SqlParameter("@Month", onedata.Month),
                            new SqlParameter("@People", onedata.People),
                            new SqlParameter("@PerShare", onedata.PerShare),
                            new SqlParameter("@StockName", onedata.StockName),
                            new SqlParameter("@StockNo", onedata.StockNo),
                             new SqlParameter("@Year", onedata.Year)
                            );

                        //DB.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0} Where CHEP = {1} AND Class = {2} AND Day = {3} AND Level = {4} AND Month = {5} AND People = {6} AND PerShare = {7} AND StockName = {8} AND StockNo = {9} AND Year = {10}",
                        //    temp.TableName,
                        //    onedata.CHEP,
                        //     onedata.Class,
                        //     onedata.Day,
                        //     onedata.Level,
                        //     onedata.Month,
                        //     onedata.People,
                        //     onedata.PerShare,
                        //     onedata.StockName,
                        //     onedata.StockNo,
                        //     onedata.Year
                        //    ), 1);

                        DB.Database.ExecuteSqlCommand(string.Format("Insert INTO {0} (CHEP , Class , Day , Level , Month , People , PerShare , StockName , StockNo , Year) VALUES (@CHEP , @Class , @Day , @Level , @Month , @People , @PerShare , @StockName , @StockNo , @Year)",temp.TableName)
                           ,
                            new SqlParameter("@CHEP", onedata.CHEP),
                            new SqlParameter("@Class", onedata.Class),
                            new SqlParameter("@Day", onedata.Day),
                            new SqlParameter("@Level", onedata.Level),
                            new SqlParameter("@Month", onedata.Month),
                            new SqlParameter("@People", onedata.People),
                            new SqlParameter("@PerShare", onedata.PerShare),
                            new SqlParameter("@StockName", onedata.StockName),
                            new SqlParameter("@StockNo", onedata.StockNo),
                             new SqlParameter("@Year", onedata.Year));

                        //DB.Database.ExecuteSqlCommand(string.Format("Insert INTO {0} Where CHEP = {1} AND Class = {2} AND Day = {3} AND Level = {4} AND Month = {5} AND People = {6} AND PerShare = {7} AND StockName = {8} AND StockNo = {9} AND Year = {10}",
                        //    temp.TableName,
                        //    onedata.CHEP,
                        //     onedata.Class,
                        //     onedata.Day,
                        //     onedata.Level,
                        //     onedata.Month,
                        //     onedata.People,
                        //     onedata.PerShare,
                        //     onedata.StockName,
                        //     onedata.StockNo,
                        //     onedata.Year
                        //    ), 1);
                    }
                }

            }

            return MyData;

        }

        /// <summary>
        /// 回傳股票比例資料表資料
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<SingleStockQueryCHEPResult> QueryData(SingleStockQueryParam Query)
        {
            //過濾搜尋時間 權重過濾法
            int StartDateSum = Query.Date_Start.Year * 1500 + Query.Date_Start.Month * 100 + Query.Date_Start.Day * 1;
            int EndDateSum = Query.Date_End.Year * 1500 + Query.Date_End.Month * 100 + Query.Date_End.Day * 1;

            using (var DB = new Model.ModelDB.BochenLinTestEntities())
            {
                //動態資料表 依照對應的資料表取出相對應的值
                var temp2 = DB.Database.SqlQuery<SingleStockQueryModel>(string.Format("SELECT * FROM {0} Where (Year * 1500 + Month *100 + Day )<= {1} AND (Year * 1500 + Month *100 + Day ) >={2}", Query.TableName , EndDateSum, StartDateSum), 1);
                
                //製作datatable表
                List<SingleStockQueryCHEPResult> Result = new List<SingleStockQueryCHEPResult>();
                SingleStockQueryCHEPResult Single = new SingleStockQueryCHEPResult();

                //排序資料列
                var temp4 = temp2.OrderByDescending(o => o.Year).ThenByDescending(o => o.Month).ThenByDescending(o => o.Day).ThenBy(o => o.Level).ToList();
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
                return Result;
            }
        }

        /// <summary>
        /// 回傳股東人數資料表資料
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<SingleStockQueryCHEPResult> QueryPeopleData(SingleStockQueryParam Query)
        {
            //過濾搜尋時間 權重過濾法
            int StartDateSum = Query.Date_Start.Year * 1500 + Query.Date_Start.Month * 100 + Query.Date_Start.Day * 1;
            int EndDateSum = Query.Date_End.Year * 1500 + Query.Date_End.Month * 100 + Query.Date_End.Day * 1;

            using (var DB = new Model.ModelDB.BochenLinTestEntities())
            {
                //動態資料表 依照對應的資料表取出相對應的值
                var temp2 = DB.Database.SqlQuery<SingleStockQueryModel>(string.Format("SELECT * FROM {0} Where (Year * 1500 + Month *100 + Day )<= {1} AND (Year * 1500 + Month *100 + Day ) >={2}", Query.TableName, EndDateSum, StartDateSum), 1);

                //製作datatable表
                List<SingleStockQueryCHEPResult> Result = new List<SingleStockQueryCHEPResult>();
                SingleStockQueryCHEPResult Single = new SingleStockQueryCHEPResult();
                
                //排序資料列
                var temp4 = temp2.OrderByDescending(o => o.Year).ThenByDescending(o => o.Month).ThenByDescending(o => o.Day).ThenBy(o => o.Level).ToList();
                foreach (var tb in temp4)
                {
                    //依照Level分類資料       
                    switch (tb.Level)
                    {
                        case 1:
                            Single.Level_1 = tb.People;
                            break;
                        case 2:
                            Single.Level_2 = tb.People;
                            break;
                        case 3:
                            Single.Level_3 = tb.People;
                            break;
                        case 4:
                            Single.Level_4 = tb.People;
                            break;
                        case 5:
                            Single.Level_5 = tb.People;
                            break;
                        case 6:
                            Single.Level_6 = tb.People;
                            break;
                        case 7:
                            Single.Level_7 = tb.People;
                            break;
                        case 8:
                            Single.Level_8 = tb.People;
                            break;
                        case 9:
                            Single.Level_9 = tb.People;
                            break;
                        case 10:
                            Single.Level_10 = tb.People;
                            break;
                        case 11:
                            Single.Level_11 = tb.People;
                            break;
                        case 12:
                            Single.Level_12 = tb.People;
                            break;
                        case 13:
                            Single.Level_13 = tb.People;
                            break;
                        case 14:
                            Single.Level_14 = tb.People;
                            break;
                        case 15:
                            Single.Level_15 = tb.People;
                            Single.ShowDate = tb.Year + "/" + tb.Month + "/" + tb.Day;
                            Single.Date = new DateTime(tb.Year, tb.Month, tb.Day);
                            Result.Add(Single);
                            Single = new SingleStockQueryCHEPResult();
                            break;
                    }
                }
                return Result;
            }
        }


        /// <summary>
        /// 傳回使用者自訂群組分類的圖表資料 - 籌碼比例
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<SingleStockQueryCHEPResult> QueryDataForExtention(SingleStockQueryExtentionKindChartParam Query)
        {

            using (var DB = new Model.ModelDB.BochenLinTestEntities())
            {
                //過濾搜尋時間 權重過濾法
                int StartDateSum = Query.Date_Start.Year * 1500 + Query.Date_Start.Month * 100 + Query.Date_Start.Day * 1;
                int EndDateSum = Query.Date_End.Year * 1500 + Query.Date_End.Month * 100 + Query.Date_End.Day * 1;

                //動態資料表 依照對應的資料表取出相對應的值
                var temp2 = DB.Database.SqlQuery<SingleStockQueryModel>(string.Format("SELECT * FROM {0} Where (Year * 1500 + Month *100 + Day )<= {1} AND (Year * 1500 + Month *100 + Day ) >={2}", Query.TableName, EndDateSum, StartDateSum), 1);

                //製作datatable表
                List<SingleStockQueryCHEPResult> Result = new List<SingleStockQueryCHEPResult>();
                SingleStockQueryCHEPResult Single = new SingleStockQueryCHEPResult();

                //排序資料列
                var temp4 = temp2.OrderByDescending(o => o.Year).ThenByDescending(o => o.Month).ThenByDescending(o => o.Day).ThenBy(o => o.Level).ToList();
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
                return Result;
            }
        }

        /// <summary>
        /// 傳回使用者自訂群組分類的圖表資料 - 股東人數
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<SingleStockQueryCHEPResult> QueryDataForPeopleExtention(SingleStockQueryExtentionKindChartParam Query)
        {

            using (var DB = new Model.ModelDB.BochenLinTestEntities())
            {
                //過濾搜尋時間 權重過濾法
                int StartDateSum = Query.Date_Start.Year * 1500 + Query.Date_Start.Month * 100 + Query.Date_Start.Day * 1;
                int EndDateSum = Query.Date_End.Year * 1500 + Query.Date_End.Month * 100 + Query.Date_End.Day * 1;

                //動態資料表 依照對應的資料表取出相對應的值
                var temp2 = DB.Database.SqlQuery<SingleStockQueryModel>(string.Format("SELECT * FROM {0} Where (Year * 1500 + Month *100 + Day )<= {1} AND (Year * 1500 + Month *100 + Day ) >={2}", Query.TableName, EndDateSum, StartDateSum), 1);

                //製作datatable表
                List<SingleStockQueryCHEPResult> Result = new List<SingleStockQueryCHEPResult>();
                SingleStockQueryCHEPResult Single = new SingleStockQueryCHEPResult();

                //排序資料列
                var temp4 = temp2.OrderByDescending(o => o.Year).ThenByDescending(o => o.Month).ThenByDescending(o => o.Day).ThenBy(o => o.Level).ToList();
                foreach (var tb in temp4)
                {
                    //依照Level分類資料       
                    switch (tb.Level)
                    {
                        case 1:
                            Single.Level_1 = tb.People;
                            break;
                        case 2:
                            Single.Level_2 = tb.People;
                            break;
                        case 3:
                            Single.Level_3 = tb.People;
                            break;
                        case 4:
                            Single.Level_4 = tb.People;
                            break;
                        case 5:
                            Single.Level_5 = tb.People;
                            break;
                        case 6:
                            Single.Level_6 = tb.People;
                            break;
                        case 7:
                            Single.Level_7 = tb.People;
                            break;
                        case 8:
                            Single.Level_8 = tb.People;
                            break;
                        case 9:
                            Single.Level_9 = tb.People;
                            break;
                        case 10:
                            Single.Level_10 = tb.People;
                            break;
                        case 11:
                            Single.Level_11 = tb.People;
                            break;
                        case 12:
                            Single.Level_12 = tb.People;
                            break;
                        case 13:
                            Single.Level_13 = tb.People;
                            break;
                        case 14:
                            Single.Level_14 = tb.People;
                            break;
                        case 15:
                            Single.Level_15 = tb.People;
                            Single.ShowDate = tb.Year + "/" + tb.Month + "/" + tb.Day;
                            Single.Date = new DateTime(tb.Year, tb.Month, tb.Day);
                            Result.Add(Single);
                            Single = new SingleStockQueryCHEPResult();
                            break;
                    }
                }
                return Result;
            }
        }


        /// <summary>
        /// 取出股票對應的中文名稱
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public string MappingName(SingleStockQueryParam Query)
        {
            using (var DB = new Model.ModelDB.BochenLinTestEntities())
            {
                var temp = DB.TDDC_Mapping;
                var get = temp.Where(o => o.TableName == Query.TableName).DefaultIfEmpty();
                return get.ToList()[0].Ch;
            }

        }
    }
}
