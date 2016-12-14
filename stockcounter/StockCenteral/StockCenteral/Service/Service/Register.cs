using Model.ViewModel.Account;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class Register : IRegister
    {
        /// <summary>
        /// 判斷是否有重複帳號
        /// </summary>
        /// <param name="UserInput"></param>
        /// <returns></returns>
        public bool AccountRepeatJudge(RegisterViewModel UserInput)
        {
            

                var _Repository = new Model.ModelDB.BochenLinTestEntities().AccountTables;
                
                return _Repository.Where(o => o.Account == UserInput.Account).Any();

        }

        /// <summary>
        /// 註冊使用者資料
        /// </summary>
        /// <param name="UserInput"></param>
        /// <returns></returns>
        public bool Add(RegisterViewModel UserInput)
        {
            try//增加註冊資料
            {
                var _Repository = new Model.ModelDB.BochenLinTestEntities();

                
                {
                    Model.ModelDB.AccountTable User = new Model.ModelDB.AccountTable();
                    User.Account = UserInput.Account;
                    User.GUID = Guid.NewGuid();
                    User.Password = UserInput.Password;
                    User.UserAddress = UserInput.UserAddress;
                    User.UserCellPhone = UserInput.UserCellPhone;
                    User.UserLevel = 2;
                    User.UserMail = UserInput.UserMail;
                    User.UserName = UserInput.UserName;
                    User.UserPhone = UserInput.UserPhone;
                    User.UserRegisterDate = DateTime.Now;
                    _Repository.AccountTables.Add(User);
                    _Repository.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           


        }
    }
}
