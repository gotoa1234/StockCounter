using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ServiceModel.Account
{
    /// <summary>
    /// 使用者的資訊表
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 登入帳號
        /// </summary>
        public string ID{ get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; }
        
    }
}
