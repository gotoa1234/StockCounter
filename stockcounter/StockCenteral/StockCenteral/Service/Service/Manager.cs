using Model.ViewModel.NewsBoard;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class Manager : IManager
    {
        /// <summary>
        /// 取出更新留言板的所有資料
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<NewsBoardViewModel> Get_Newboards()
        {
            using (var DB = new Model.ModelDB.BochenLinTestEntities())
            {
                return DB.NewBoard.Select(o => new NewsBoardViewModel { Guid = o.Guid, Datetime = o.Datetime, Kind = o.Kind, Message = o.Message, Note = o.Note, ShowInfom = o.ShowInfom, Title = o.Title }).ToList();
            }

        }

        /// <summary>
        /// 取出更新留言板該筆資料 
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public NewsBoardViewModel Get_rowdata(string GUID)
        {
            using (var DB = new Model.ModelDB.BochenLinTestEntities())
            {
                var GetRow = DB.NewBoard.Where(o => o.Guid.ToString() == GUID).FirstOrDefault();
                return new NewsBoardViewModel { Guid = GetRow.Guid, Datetime = GetRow.Datetime, Kind = GetRow.Kind, Message = GetRow.Message, Note = GetRow.Note, ShowInfom = GetRow.ShowInfom, Title = GetRow.Title }; ;
            }

        }


        /// <summary>
        /// 更新留言板該筆資料
        /// </summary>
        /// <param name="Query"></param>
        /// <returns>成功或失敗</returns>
        public string Edit_rowdata(Model.ViewModel.NewsBoard.NewsBoardViewModel RowData)
        {
            try
            {
                using (var DB = new Model.ModelDB.BochenLinTestEntities())
                {
                    var GetRow = DB.NewBoard.Where(o => o.Guid.ToString() == RowData.Guid.ToString()).FirstOrDefault();
                    GetRow.Kind = RowData.Kind;
                    GetRow.Message = RowData.Message;
                    if (RowData.Note == null)
                        GetRow.Note = "";
                    else
                        GetRow.Note = RowData.Note;

                    GetRow.ShowInfom = RowData.ShowInfom;
                    GetRow.Title = RowData.Title;
                    GetRow.Datetime = RowData.Datetime;
                    DB.SaveChanges();
                    return "修改成功!";
                }
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }

        /// <summary>
        /// 新增留言板該筆資料
        /// </summary>
        /// <param name="Query"></param>
        /// <returns>成功或失敗</returns>
        public string Add_rowdata(Model.ViewModel.NewsBoard.NewsBoardViewModel RowData)
        {
            try
            {
                using (var DB = new Model.ModelDB.BochenLinTestEntities())
                {
                    Model.ModelDB.NewBoard NewRowData = new Model.ModelDB.NewBoard();
                    NewRowData.Guid = Guid.Parse(Guid.NewGuid().ToString("D"));
                    NewRowData.Kind = RowData.Kind;
                    NewRowData.Message = RowData.Message;
                    if (RowData.Note == null)
                        NewRowData.Note = "";
                    else
                        NewRowData.Note = RowData.Note;

                    NewRowData.ShowInfom = RowData.ShowInfom;
                    NewRowData.Title = RowData.Title;
                    NewRowData.Datetime = RowData.Datetime;
                    DB.NewBoard.Add(NewRowData);
                    DB.SaveChanges();
                    return "新增成功!";
                }
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }


        /// <summary>
        /// 移除留言板的該筆資料
        /// </summary>
        /// <param name="RowData"></param>
        /// <returns></returns>
        public string Delete_rowdata(string GUID)
        {
            try
            {
                using (var DB = new Model.ModelDB.BochenLinTestEntities())
                {
                    var GetRow = DB.NewBoard.Where(o => o.Guid.ToString() == GUID).FirstOrDefault();
                    DB.NewBoard.Remove(GetRow);
                    DB.SaveChanges();
                    return "移除成功";
                }
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }
    }
}
