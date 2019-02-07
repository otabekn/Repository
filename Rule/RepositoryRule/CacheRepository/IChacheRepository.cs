using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryRule.CacheRepository
{
    public interface IChacheRepository<T>
        where T:class
    {
        #region Find
        T Find(string id);
        T FindFirst(string id);
        T FindFirst(string field, string value);
        T FindFirst();
        T FindFirst(Expression<Func<T, bool>> selector);

        #region FindAsync
        Task<T> FindFirstAsync(string id);
        Task<T> FindAsync(string id);
        Task<T> FindFirstAsync(string field, string value);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> selector);
        Task<T> FirstAsync();
        #endregion

        #endregion
        #region Add
        void Add(string id,T model);
        #endregion 
        #region Update
        void Update(string id, T modal);
        #endregion
        #region UpdateAsync
        Task UpdateAsync(T model);
        #endregion
        #region Remove
        void Remove(string id);
        void CatcheDelete(string text, string id,T model);
        #endregion


    }
}
