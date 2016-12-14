using Model.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class GenericRepository<C, T> : IGenericRepository<C, T>//---定義資料庫存取方式 <資料庫C , 資料表T>
        where T : class
        where C : DbContext, new()
    {

        private C _entities = new C();
        public C Context//--------------------回傳entity 的相對應型別
        {

            get { return _entities; }
            set { _entities = value; }
        }

        private DbSet<T> dbSet { get { return this._entities.Set<T>(); } }//---使用DbSet 方法存取Context資料庫

        public IQueryable<M> Query<M>() where M : class //---------------------資料表中的屬性 (從entity取回存放到本機上)
        {
            IQueryable<M> query = _entities.Set<M>();

            return query;
        }


        public IQueryable<T> GetAll()//----------------------------------------取得所有資料表資料
        {

            return this.dbSet;
        }



        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)//--同步式的取得資料
        {
            return await this.dbSet.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)//尋找相對應的資料 
        {

            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }


        public async Task<bool> Add(T entity)//----增加資料
        {
            bool result = true;
            this.dbSet.Add(entity);


            await this._entities.SaveChangesAsync();
            return result;
        }

        public async Task<bool> Delete(T entity)//--刪除資料
        {
            bool result = true;
            this.dbSet.Remove(entity);

            await this._entities.SaveChangesAsync();
            return result;
        }


        public async Task<bool> Edit(T entity)//------修改資料
        {
            bool result = true;
            this._entities.Entry(entity).State = EntityState.Modified;
            await this._entities.SaveChangesAsync();
            return result;
        }

        public void Save()//-----------------儲存資料
        {
            _entities.SaveChanges();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)//---用同步的方式取得資料並且存放到List中
        {

            return await this.dbSet.Where(predicate).ToListAsync();
        }

        public void Dispose()//--------釋放記憶體 本機
        {
            this.Dispose(true); GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)//---釋放記憶體 entity 
        {
            if (disposing)
            {
                if (this._entities != null)
                {
                    this._entities.Dispose();
                    this._entities = null;
                }
            }
        }
    }
}
