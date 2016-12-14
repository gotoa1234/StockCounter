using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Model.IRepository
{
    public interface IGenericRepository<C, T> : IDisposable where T : class
    {
        //=================== 定義介面 實作方法在 Repository/GeneriRepository.cs 中
        IQueryable<M> Query<M>() where M : class;
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        void Dispose();
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<bool> Add(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Edit(T entity);
        void Save();
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

    }
}
