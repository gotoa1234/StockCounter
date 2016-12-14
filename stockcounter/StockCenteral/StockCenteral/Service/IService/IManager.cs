using Model.QueryModel;
using Model.ViewModel.NewsBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IManager
    {
        List<Model.ViewModel.NewsBoard.NewsBoardViewModel> Get_Newboards();

        NewsBoardViewModel Get_rowdata(string GUID);

        string Edit_rowdata(Model.ViewModel.NewsBoard.NewsBoardViewModel RowData);

        string Add_rowdata(Model.ViewModel.NewsBoard.NewsBoardViewModel RowData);

        string Delete_rowdata(string GUID);
    }
}
