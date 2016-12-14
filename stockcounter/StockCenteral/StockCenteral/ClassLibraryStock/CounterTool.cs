using ClassLibraryStock.OriClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data.SqlClient;

namespace ClassLibraryStock
{
    /// <summary>
    /// 收集集保戶股權資料
    /// </summary>
    public class CounterTool
    {
        //建立Logger工具
        LoggerTool Tool = new LoggerTool();
        public CounterTool()
        {

        }

        /// <summary>
        /// 寫入該週的資料到自己的資料表
        /// 說明：每股都是獨立的資料表
        /// </summary>
        ///<param name = "ThisWeek" >true = 只加入本周 false =所有資料表更新</ param >
        public void Add(bool ThisWeek)
        {
            int AlreadyInsert = 0;//已經插入的資料數量 累計
            try
            {
                WebClient client = new WebClient();//------Client
                List<string> TddcDate = StockDateTable();//取得集保戶日期
                if (ThisWeek)
                {
                    var temp = TddcDate.ElementAt(0);
                    TddcDate.Clear();
                    TddcDate.Add(temp);
                }


                foreach (var No in StockNoTable())//將每個代號都視為獨立的資料表
                {
                    List<Counter> Detail = new List<Counter>();

                    foreach (var Date in TddcDate)
                    {
                        #region 取得該StockNo 的Datalist

                        try
                        {
                            MemoryStream ms = new MemoryStream(client.DownloadData(ConfigurationManager.AppSettings["TDDC"].ToString() + "?SCA_DATE=" + Date + "&&SqlMethod=StockNo&StockNo=" + No + "&StockName=&sub=%ACd%B8%DF"));

                            // 使用預設編碼讀入 HTML 
                            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                            doc.Load(ms, Encoding.Default);

                            // 裝載第一層查詢結果 
                            HtmlAgilityPack.HtmlDocument docStockContext = new HtmlAgilityPack.HtmlDocument();
                            HtmlNodeCollection nodeHeaders;

                            // 每筆資料
                            Counter Single = new Counter();

                            for (int SQ = 2; SQ < 17; SQ++)
                            {
                                //-- 該網頁的Xpath語法 
                                try
                                {
                                    ///html[1]/body[1]/table[1]/tr[1]/td[1]/table[6]/tbody[1]/tr[1]
                                    docStockContext.LoadHtml(doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/table[1]/tr[1]/td[1]/table[6]/tbody[1]/tr[" + SQ + "]").InnerHtml);
                                    // 取得日期分割
                                }
                                catch
                                {
                                    continue;//--該筆資料不能取得 
                                }
                                Single = new Counter();

                                for (int s = 1; s < 6; s++)
                                {

                                    nodeHeaders = docStockContext.DocumentNode.SelectNodes("./td[" + s.ToString() + "]");
                                    string[] values = docStockContext.DocumentNode.SelectSingleNode("./td[" + s.ToString() + "]").InnerText.Trim().Split('\n');
                                    switch (s)
                                    {
                                        case 1:
                                            Single.Level = int.Parse(values[0].ToString());
                                            break;
                                        case 2:
                                            Single.Class = values[0];
                                            break;
                                        case 3:
                                            Single.People = int.Parse(values[0].ToString().Replace(",", ""));
                                            break;
                                        case 4:
                                            long Temp_PerShare = long.Parse(values[0].ToString().Replace(",", ""));

                                            Single.PerShare = Convert.ToInt32(Temp_PerShare / 1000);//----股數變張數 
                                            break;
                                        case 5:
                                            Single.CHEP = float.Parse(values[0].ToString());
                                            break;

                                    }


                                }
                                //--插入進資料庫 這裡的資料一定為唯一的

                                Single.StockNo = No;//股票代號
                                Single.StockName = No;//股票名稱
                                Single.Year = int.Parse(Date.Substring(0, 4));
                                Single.Month = int.Parse(Date.Substring(4, 2));
                                Single.Day = int.Parse(Date.Substring(6, 2));
                                Detail.Add(Single);

                            }
                        }
                        catch (Exception ex)
                        {
                            this.Tool.LoggerTool_Add(new Logger("Error", DateTime.Now, ex.Message, ex.StackTrace));
                        }
                        #endregion
                    }

                    //取完後要將該表資料 Transaction 到DB
                    if (Detail.Count > 0)
                    {
                        AlreadyInsert += WriteSqlServer(Detail);
                    }
                }

            }
            catch (Exception ex)
            {
                this.Tool.LoggerTool_Add(new Logger("Error", DateTime.Now, ex.Message, ex.StackTrace));
            }


            ///
            this.Tool.LoggerTool_Add(new Logger("Info", DateTime.Now, "已經順利執行插入資料 共：" + AlreadyInsert, ""));



        }


        /// <summary>
        /// 將股票所有代號傳回
        /// </summary>
        public List<string> StockNoTable()
        {
            List<string> StockNo = new List<string>();
            //---先將日期從該網站撈取
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(ConfigurationManager.AppSettings["TDDC_new"].ToString()));

            //將資料取得 20160205,0050,2,48723,98160288,7.06
            string document = System.Text.Encoding.UTF8.GetString(ms.ToArray());
            string[] stringSeparators = new string[] { "\r\n" };
            var get = document.Split(stringSeparators, StringSplitOptions.None);

            foreach (string str in get)
            {
                try
                {
                    StockNo.Add(str.Split(',')[1]);
                }
                catch
                {
                    continue;
                }
            }

            StockNo = (from Table in StockNo
                       select Table).Distinct().ToList();

            return StockNo;
        }

        /// <summary>
        /// 將集保戶網站定義的股票日期傳回
        /// </summary>
        /// <returns></returns>
        public List<string> StockDateTable()
        {

            List<string> Date = new List<string>();

            //---先將日期從該網站撈取
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(ConfigurationManager.AppSettings["TDDC"].ToString()));

            // 使用預設編碼讀入 HTML 

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(ms, Encoding.Default);

            // 裝載第一層查詢結果 
            HtmlAgilityPack.HtmlDocument docStockContext = new HtmlAgilityPack.HtmlDocument();
            HtmlNodeCollection nodeHeaders;

            //-- 該網頁的Xpath語法
            docStockContext.LoadHtml(doc.DocumentNode.SelectSingleNode("html[1]/body[1]/table[1]/tr[1]/td[1]/table[2]/tbody[1]/tr[1]/td[2]/table[1]/tr[1]/td[1]").InnerHtml);

            // 取得日期分割
            nodeHeaders = docStockContext.DocumentNode.SelectNodes("./select[1]");
            string[] values = docStockContext.DocumentNode.SelectSingleNode("./select[1]").InnerText.Trim().Split('\n');

            // 日期分割規則 : 8bit 為一單位
            int start = 0;
            int end = 8;
            while (start < values[0].Length)
            {
                string Div = values[0].Substring(start, end);
                start += end;
                Date.Add(Div);
            }

            return Date;
        }

        /// <summary>
        /// 寫進SqlServer中 用transcation
        /// </summary>
        /// <param name="MyData"></param>
        public int WriteSqlServer(List<Counter> MyData)
        {

            List<Counter> AddInsert = new List<Counter>();//要加入的Insert資料
            CheckData(MyData[0].StockNo);//檢查是否有該表在資料庫，沒有就要加入

            SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EocConnection"].ToString());


            string TableName = "TDDC_" + MyData[0].StockNo;

            db.Open();


            try
            {
                #region 先從資料庫比對篩選需要加入的資料


                //1.先判斷是否資料庫有該資料
                using (SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["EocConnection"].ToString()))
                {
                    sqlConnection1.Open();
                    foreach (var Single in MyData)
                    {
                        using (SqlCommand cmd = new SqlCommand(string.Format("SELECT Top(1) * FROM " + TableName + " where Level = '{0}' AND Class ='{1}' AND StockNo ='{2}'  AND Year ='{3}'  AND Month ='{4}'  AND Day ='{5}'", Single.Level, Single.Class, Single.StockNo, Single.Year, Single.Month, Single.Day), sqlConnection1))
                        {
                            // Data is accessible through the DataReader object here.
                            var temp = cmd.ExecuteScalar();
                            if (temp == null)
                            {
                                AddInsert.Add(Single);
                            }
                        }

                    }
                    sqlConnection1.Close();
                }
                #endregion

                #region 將需要加入的資料進行Transaction  

                using (SqlConnection sqlConnectionTrans = new SqlConnection(ConfigurationManager.ConnectionStrings["EocConnection"].ToString()))
                {
                    SqlTransaction transaction;//
                    sqlConnectionTrans.Open();
                    transaction = sqlConnectionTrans.BeginTransaction();


                    try
                    {
                        foreach (Counter single in AddInsert)
                        {
                            String query = "INSERT INTO " + TableName + " (Level, Class, People, PerShare, CHEP, StockNo, StockName, Year, Month, Day) VALUES(@level, @class, @people, @perShare, @chep, @stockNo, @stockName, @year, @month, @day)";

                            SqlCommand command = new SqlCommand(query, sqlConnectionTrans, transaction);
                            command.Parameters.Add("@level", single.Level);
                            command.Parameters.Add("@class", single.Class);
                            command.Parameters.Add("@people", single.People);
                            command.Parameters.Add("@perShare", single.PerShare);
                            command.Parameters.Add("@chep", single.CHEP);
                            command.Parameters.Add("@stockNo", single.StockNo);
                            command.Parameters.Add("@stockName", single.StockName);
                            command.Parameters.Add("@year", single.Year);
                            command.Parameters.Add("@month", single.Month);
                            command.Parameters.Add("@day", single.Day);

                            command.ExecuteNonQuery();

                        }

                        transaction.Commit();
                        sqlConnectionTrans.Close();
                    }
                    catch (SqlException sqlerror)
                    {
                        //發生資料插入錯誤
                        this.Tool.LoggerTool_Add(new Logger("Error", DateTime.Now, sqlerror.Message, sqlerror.StackTrace));
                    }

                }


                #endregion

            }
            catch (SqlException sqlError)
            {
                //other error
                this.Tool.LoggerTool_Add(new Logger("Error", DateTime.Now, sqlError.Message, sqlError.StackTrace));
            }
            db.Close();


            return AddInsert.Count();
        }


        /// <summary>
        /// 檢查是否存在該資料表
        /// </summary>
        /// <param name="StockNo">將股價做為表的查詢</param>
        public void CheckData(string StockNo)
        {
            try
            {

                string connetionString = null;
                SqlConnection con;
                SqlCommand command;
                connetionString = ConfigurationManager.ConnectionStrings["EocConnection"].ToString();
                con = new SqlConnection(connetionString);
                string TableName = "TDDC_" + StockNo;
                string CreateTable = "CREATE TABLE " + TableName + " (Level int not null , Class nvarchar(30) not null, People  int not null , PerShare int not null ,CHEP float not null , StockNo  nvarchar(30) not null,StockName nvarchar(30) not null,  Year int not null, Month int not null ,Day int not null)"; ;
                try
                {
                    con.Open();
                    command = new SqlCommand(CreateTable, con);
                    command.ExecuteReader();
                    con.Close();
                    ///因為增加該筆資料了 所以增加對應項目
                    CheckMapping(StockNo);
                }
                catch (Exception ex)
                {
                    con.Close();//已經存在該表
                }

            }
            catch (Exception ex)
            {
                this.Tool.LoggerTool_Add(new Logger("Error", DateTime.Now, ex.Message, ex.StackTrace));
            }

        }

        /// <summary>
        /// 將資料表的名稱存到股票名稱對應表中
        /// </summary>
        /// <param name="StockNo"></param>
        public void CheckMapping(string StockNo)
        {
            string TableName = "TDDC_Mapping";

            #region 建立資料
            try
            {
                string connetionString = null;
                SqlConnection con;
                SqlCommand command;
                connetionString = ConfigurationManager.ConnectionStrings["EocConnection"].ToString();
                con = new SqlConnection(connetionString);
                string CreateTable = "CREATE TABLE " + TableName + " (StockNo nvarchar(30) not null, TableName nvarchar(30) not null, Ch nvarchar(30) not null, State nvarchar(30) not null)"; ;
                try
                {
                    con.Open();
                    command = new SqlCommand(CreateTable, con);
                    command.ExecuteReader();
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();//已經存在該表
                }
            }
            catch (Exception ex)
            {
                this.Tool.LoggerTool_Add(new Logger("Error", DateTime.Now, ex.Message, ex.StackTrace));
            }
            #endregion

            #region 增加指定的資料

            try
            {
                using (SqlConnection sqlConnectionTrans = new SqlConnection(ConfigurationManager.ConnectionStrings["EocConnection"].ToString()))
                {
                    sqlConnectionTrans.Open();
                    try
                    {

                        String query = "INSERT INTO " + TableName + " (StockNo, TableName, Ch, State)  VALUES(@StockNo, @TableName, @Ch, @State)";

                        SqlCommand command = new SqlCommand(query, sqlConnectionTrans);
                        command.Parameters.Add("@StockNo", StockNo);
                        command.Parameters.Add("@TableName", "TDDC_" + StockNo);
                        command.Parameters.Add("@Ch", "尚未更新");
                        command.Parameters.Add("@State", "尚未更新");
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException sqlerror)
                    {
                        //發生資料插入錯誤
                        this.Tool.LoggerTool_Add(new Logger("Error", DateTime.Now, sqlerror.Message, sqlerror.StackTrace));
                    }

                }

            }
            catch (Exception ex)
            {
                this.Tool.LoggerTool_Add(new Logger("Error", DateTime.Now, ex.Message, ex.StackTrace));
            }

            #endregion

        }


    }   
}
