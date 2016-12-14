using HtmlAgilityPack;
using Model.ModelDB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryStock
{
    public class ISIN
    {
        //建立Logger工具 
        LoggerTool Tool = new LoggerTool();
        //Filter String
        string[] FilterStock = { "股票", "上市認購(售)權證", "特別股", "ETF", "臺灣存託憑證", "受益證券-不動產投資信託", "管理股票", "上櫃認購(售)權證", "受益證券-資產基礎證券" };

        /// <summary>
        /// TDDC_Mapping 該表更新 將對應的中文名稱與股票代號帶入
        /// </summary>
        public void run()
        {
            try
            {
                string LocationState = "";
                WebClient client = new WebClient();//------Client
                List<TDDC_Mapping> ISINCollection = new List<TDDC_Mapping>();//Build empty mapping table

                #region 取得台股當前對應代號與中文名稱

                int[] Modeenum = { 2, 4, 5 };
                // 2= 上市櫃 4=上櫃 5=興櫃
                try
                {
                    
                    for (int tag = 0; tag <3; tag++)
                    {
                        int Mode = Modeenum[tag];
                        MemoryStream ms = new MemoryStream(client.DownloadData(ConfigurationManager.AppSettings["Stock_IsIn"].ToString() + Mode.ToString()));

                        // 使用預設編碼讀入 HTML 
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.Load(ms, Encoding.Default);

                        // 裝載第一層查詢結果 
                        HtmlAgilityPack.HtmlDocument docStockContext = new HtmlAgilityPack.HtmlDocument();
                        HtmlNodeCollection nodeHeaders;

                        // 每筆資料
                        TDDC_Mapping Single = new TDDC_Mapping();

                        for (int SQ = 1; ; SQ++)
                        {
                            //-- 該網頁的Xpath語法 
                            try
                            {
                                //擷取資料
                                docStockContext.LoadHtml(doc.DocumentNode.SelectSingleNode("/body[1]/table[2]/tr[" + SQ + "]").InnerHtml);
                            }
                            catch
                            {
                                break; //當不能擷取內部資料時表示該跳出了
                            }
                            Single = new TDDC_Mapping();

                            nodeHeaders = docStockContext.DocumentNode.SelectNodes("./td[1]");
                            string[] values = docStockContext.DocumentNode.SelectSingleNode("./td[1]").InnerText.Trim().Split('　');

                            //過濾名詞
                            if (filter(values[0], Modeenum[tag]) != "PASS")
                            {
                                LocationState = filter(values[0], Modeenum[tag]);
                            }
                            

                            //過濾代號與中文名稱
                            for (int i = 0; i < values.Count(); i++)
                            {
                                if (i == 0)
                                    Single.StockNo = values[i].Trim();
                                else
                                    Single.Ch = values[i].Trim();
                            }

                            //插入型態
                            Single.State = LocationState;

                            //--插入進資料庫 這裡的資料一定為唯一的
                            ISINCollection.Add(Single);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Tool.LoggerTool_Add(new ClassLibraryStock.OriClass.Logger("Error", DateTime.Now, ex.Message, ex.StackTrace));
                }
                #endregion


                //寫入資料庫 - 強制更新Table
                ISINUpDateForSqlServer(ISINCollection);
            }
            catch (Exception ex)
            {
                this.Tool.LoggerTool_Add(new ClassLibraryStock.OriClass.Logger("Error", DateTime.Now, ex.Message, ex.StackTrace));
            }

            
            ///
            this.Tool.LoggerTool_Add(new ClassLibraryStock.OriClass.Logger("Info", DateTime.Now, "順利更新TDDC_Mapping資料表",""));

        }


        /// <summary>
        /// 寫進SqlServer中 用transcation
        /// </summary>
        /// <param name="MyData"></param>
        public void ISINUpDateForSqlServer(List<TDDC_Mapping> MyData)
        {
            string TableName = "TDDC_Mapping";
            try
            {

                #region 更新Mapping資料 Transaction  

                using (SqlConnection sqlConnectionTrans = new SqlConnection(ConfigurationManager.ConnectionStrings["EocConnection"].ToString()))
                {
                    SqlTransaction transaction;//
                    sqlConnectionTrans.Open();
                    transaction = sqlConnectionTrans.BeginTransaction();

                    try
                    {
                        foreach (var single in MyData)
                        {
                            if (single.Ch == null || single.StockNo == null)
                                continue;

                            String query = "IF EXISTS (SELECT * FROM " + TableName +" WHERE StockNo = "+"'"+single.StockNo+ "'"+ ") ";
                            query += " UPDATE  " + TableName + " SET Ch = @Ch , "+" State = @State " + " Where StockNo = @StockNo";
                            
                            SqlCommand command = new SqlCommand(query, sqlConnectionTrans, transaction);
                            command.Parameters.Add("@Ch", single.Ch);
                            command.Parameters.Add("@State", single.State);
                            command.Parameters.Add("@StockNo", single.StockNo);
                            
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        sqlConnectionTrans.Close();
                    }
                    catch (SqlException sqlerror)
                    {
                        //發生資料插入錯誤
                        this.Tool.LoggerTool_Add(new ClassLibraryStock.OriClass.Logger("Error", DateTime.Now, sqlerror.Message, sqlerror.StackTrace));
                    }

                }


                #endregion

            }
            catch (SqlException sqlError)
            {
                //other error
                this.Tool.LoggerTool_Add(new ClassLibraryStock.OriClass.Logger("Error", DateTime.Now, sqlError.Message, sqlError.StackTrace));
            }

        }

        /// <summary>
        /// 當前的過濾資料 1.股票 2.
        /// </summary>
        /// <param name="FilterString"></param>
        public string filter(string FilterString , int mode)
        {
            if (FilterStock.Where(o => o == FilterString).Count() > 0)
            {
                if (FilterString == "股票")
                {
                    if (mode == 2)
                    {
                        return "上市";
                    }
                    else if (mode == 4)
                    {
                        return "上櫃";
                    }
                    else if (mode == 5)
                    {
                        return "興櫃";
                    }
                }
                return FilterString;
            }
            else
            {
                return "PASS";
            }
        }
    }
}
