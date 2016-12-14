using Model.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IRegister
    {
        bool AccountRepeatJudge(RegisterViewModel UserInput);
        bool Add(RegisterViewModel UserInput);
    }
}
