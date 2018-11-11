using ClassLibraryStock;
using ClassLibraryStock.OriClass;
using System;

namespace GatherCounter
{
    /// <summary>
    /// 收集籌碼資料主程式
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        { 
            //Logger tool
            LoggerTool Logger = new LoggerTool();

           //蒐集籌碼資料
            try
            {
                //ISIN Struct = new ISIN();
                //Struct.run();

                CounterTool Build = new CounterTool();
                ////收集籌碼的資料 - true =只加入本周 false = 全部加入
                Build.Add();
            }
            catch (Exception ex)
            {
                Logger.LoggerTool_Add(new Logger("Error", DateTime.Now , ex.Message, ex.StackTrace) );
            }
            
        }
    }
}
