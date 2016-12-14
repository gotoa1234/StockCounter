using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class Dividend_1 : IDividend_1
    { 
        //== 實作方法的撰寫
       // private IGenericRepository<AddressDbContext, TDDC_CounterController> _Repository;
        // Name = 帶入的帳號名稱
        //public List<TDDC_CounterController> GetPagedData(int rowNum, int pageNum, out int totalRecords, string sord, string Name)
       // {
            //using (this._Repository = new GenericRepository<AddressDbContext, TDDC_CounterController>())
            //{
            //    IQueryable<TDDC_CounterController> query = _Repository.GetAll();


            //    var list = (from data in query where data.Name == Name select data);

            //    totalRecords = list.Count();//----資料的總數

            //    if (sord == "asc")
            //        return list.OrderBy(o => o.Time).Skip(rowNum * (pageNum - 1)).Take(rowNum).ToList();
            //    else
            //        return list.OrderByDescending(o => o.Time).Skip(rowNum * (pageNum - 1)).Take(rowNum).ToList();
            //}
         //   totalRecords = 0;
           // return null;
        //}
    }
}
