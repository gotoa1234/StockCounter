using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryStock.OriClass
{
    /// <summary>
    /// 紀錄Log到DB
    /// </summary>
    public class Logger
    {
        public Logger(string level , DateTime date , string message,string stack)
        {
            this.Level = level;
            this.Date = date;
            this.Message = message;
            this.Stack = stack;
        }

        /// <summary>
        /// 紀錄層級
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 紀錄時間
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 紀錄訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 紀錄堆疊
        /// </summary>
        public string Stack { get; set; }
    }
}
