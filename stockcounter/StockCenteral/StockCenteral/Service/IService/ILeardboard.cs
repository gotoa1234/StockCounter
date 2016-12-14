using Model.QueryModel;
using Model.ViewModel.Leardboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    
    public interface ILeardboard
    {
        List<LeaderboardTDDCModel> QueryData(LeaderboardTDDCQueryParam Query);
    }
}
