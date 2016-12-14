using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Service.IService
{
    public interface IAccount
    {
        //----以下為工作的方法宣告
        bool GetPaing(string UserName, string Password);//---取得登入資訊

        bool GetUserState(string UserName, string Password);//-回傳帳號權限
    }
}
