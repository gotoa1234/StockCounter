
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ServiceModel.Account;
using Service.IService;

//AccountTables 帳號資料
namespace Service.Service
{
    public class Account : IAccount
    {
        /// <summary>
        /// 實作登入的方法
        /// </summary>
        /// <param name="Account"></param> 帳號
        /// <param name="Password"></param>密碼
        /// <returns></returns>
        public bool GetPaing(string UserName, string Password)//--使用者登入時會使用的方法
        {
           
            var _Repository = new Model.ModelDB.BochenLinTestEntities().AccountTables;

            //--判斷是否有該帳號
            if (_Repository.Where(o => o.Account == UserName && o.Password == Password).Count() != 0)
                return true;

            //---沒有該帳號就回傳false
            return false;
        }

        /// <summary>
        /// 再傳入一次帳號密碼 回傳使用者的權限大小
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool GetUserState(string UserName, string Password)//--使用者登入時會使用的方法
        {
            var _Repository = new Model.ModelDB.BochenLinTestEntities().AccountTables;

            //var temp = new Model.ModelDB.BochenLinTestEntities().procedure();

            //--判斷是否有該帳號
            if (_Repository.Where(o => o.Account == UserName && o.Password == Password).Count() != 0)
                return true;
   

            //---沒有該帳號就回傳false
            return false;
        }

        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public Model.ServiceModel.Account.Account UserInfomation(string Account)
        {
            var _Repository = new Model.ModelDB.BochenLinTestEntities().AccountTables;

            //--取得帳號裡的資料
            var temp = _Repository.Where(o => o.Account == Account).FirstOrDefault();
            Model.ServiceModel.Account.Account Result = new Model.ServiceModel.Account.Account();
            Result.ID = temp.Account;
            Result.UserName = temp.UserName;

            //回傳
            return Result;
        }

    }
}
