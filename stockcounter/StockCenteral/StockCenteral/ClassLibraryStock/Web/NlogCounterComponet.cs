using ClassLibraryStock.OriClass;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryStock.Web
{
    public class NlogCounterComponet
    {

        //建構式-可插入資料
        public NlogCounterComponet()
        {
        }

        public List<Counter> getNlogData(string ID , ParallelQuery<Counter> nowDbDateList)
        {
            List<Counter> insertData = new List<Counter>();

            string path = string.Format("http://stock.nlog.cc/c/{0}/1", ID);
            var url = path;
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);

            for (int i_tr = 2; i_tr < 50; i_tr++)
            {
                Counter item = null;
                int item_year = 0;
                int item_month = 0;
                int item_day = 0;
                //td
                for (int i_td = 1; i_td <= 16; i_td++)
                {
                    //如果已經有資料，可以跳往下個週期的資料
                    if (nowDbDateList.Where(o => o.Year == item_year && o.Month == item_month && o.Day == item_day).Any() == true)
                        break;
                    //>=2是 Hard Code 目前Nlog 的i_td 1 是日期時間，所以沒日期就可以放棄該Tr
                    if (i_td >= 2 && item_year == 0 && item_day == 0  && item_month == 0)
                        break;


                    string strPath = string.Format("/html[1]/body[1]/center[1]/div[1]/table[3]/tr[1]/td[1]/table[1]/tr[1]/td[1]/table[1]/tr[{0}]/td[{1}]",
                        i_tr,
                        i_td
                        );
                    var data = doc.DocumentNode.SelectSingleNode(strPath);

                    if (data == null)
                        continue;
                    //取得股東人數
                    string peoplepath = string.Format("http://stock.nlog.cc/c/{0}/2", ID);
                    var webPeople = new HtmlWeb
                    {
                        AutoDetectEncoding = false,
                        OverrideEncoding = Encoding.UTF8
                    };
                    HtmlAgilityPack.HtmlDocument peopleDoc = webPeople.Load(peoplepath);
                    string strPeoplePath = string.Format("/html[1]/body[1]/center[1]/div[1]/table[3]/tr[1]/td[1]/table[1]/tr[1]/td[1]/table[1]/tr[{0}]/td[{1}]",
                        i_tr,
                        i_td
                        );
                    var peopleData = peopleDoc.DocumentNode.SelectSingleNode(strPeoplePath);

                    //紀錄該筆年、月、日
                    item = new Counter()
                    {
                        Year = item_year,
                        Month = item_month,
                        Day = item_day,
                        StockName = ID,
                        StockNo = ID,
                    };
                    //取得Td的資料
                    string nowText = doc.DocumentNode.SelectSingleNode(strPath).InnerText;//籌碼
                    string nowPeopleText = peopleDoc.DocumentNode.SelectSingleNode(strPeoplePath).InnerText; //股東人數
                    //紀錄
                    if (i_td >= 2 && i_td <= 16)
                    {
                        item.CHEP = double.Parse(nowText);
                        item.People = int.Parse(nowPeopleText);
                    }
                    switch (i_td)
                    {
                        //年月日
                        case 1:
                            int temp = 0;
                            int.TryParse(nowText.Substring(0, 4), out temp);
                            item_year = temp;
                            int.TryParse(nowText.Substring(4, 2), out temp);
                            item_month = temp;
                            int.TryParse(nowText.Substring(6, 2), out temp);
                            item_day = temp;
                            break;
                        //Class 1 1-999 
                        case 2:
                            item.Class = "1-999";
                            item.Level = 1;
                            break;
                        case 3:
                            item.Class = "1,000-5,000";
                            item.Level = 2;
                            break;
                        case 4:
                            item.Class = "5,001-10,000";
                            item.Level = 3;
                            break;
                        case 5:
                            item.Class = "10,001-15,000";
                            item.Level = 4;
                            break;
                        case 6:
                            item.Class = "15,001-20,000";
                            item.Level = 5;
                            break;
                        case 7:
                            item.Class = "20,001-30,000";
                            item.Level = 6;
                            break;
                        case 8:
                            item.Class = "30,001-40,000";
                            item.Level = 7;
                            break;
                        case 9:
                            item.Class = "40,001-50,000	";
                            item.Level = 8;
                            break;
                        case 10:
                            item.Class = "50,001-100,000";
                            item.Level = 9;
                            break;
                        case 11:
                            item.Class = "100,001-200,000";
                            item.Level = 10;
                            break;
                        case 12:
                            item.Class = "200,001-400,000";
                            item.Level = 11;
                            break;
                        case 13:
                            item.Class = "400,001-600,000";
                            item.Level = 12;
                            break;
                        case 14:
                            item.Class = "600,001-800,000";
                            item.Level = 13;
                            break;
                        case 15:
                            item.Class = "800,001-1,000,000";
                            item.Level = 14;
                            break;
                        case 16:
                            item.Class = "1,000,001以上";
                            item.Level = 15;
                            break;
                        default:
                            break;
                    }
                    
                    insertData.Add(item);
                    
                }
                
            }
            //回傳找到的資料
            return insertData.Where(o => o.Year >= 0 && o.Month >= 1 && o.Day >= 1).ToList();
        }
    }
}
