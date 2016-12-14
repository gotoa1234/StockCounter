using Model.POCO.Account;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Context
{
    /// <summary>
    /// ConnectTion DB 
    /// </summary>
    public class AddressDbContext : DbContext
    {
        public AddressDbContext()
            : base(ConfigurationManager.ConnectionStrings["BochenConnection"].ToString())
        {
            Database.SetInitializer<AddressDbContext>(null);
        }
        #region 將資料庫的資料表做繫節
        public DbSet<AccountTable> AccountTable { get; set; } //--帳號表
        #endregion


    }
}
