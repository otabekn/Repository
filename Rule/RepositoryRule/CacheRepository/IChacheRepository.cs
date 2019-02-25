using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryRule.CacheRepository
{
    public interface ICacheRepository<T>
        where T:class
    {
        #region Find
        T Find(string id);
        T FindFirst(string id);
        T Find(Expression<Func<T, bool>> seletor);
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
        Task AddAsync(string id, T model);
        void AddRange(List<T> models);
        Task AddRangeAsync(List<T> models);
        #endregion
        #region Update
        void Update(Expression<Func<T, T>>selector);
        Task Update(T model);
        void Update(List<T>models);
        void Update(string id, T modal);
        #endregion
        #region UpdateAsync
        Task UpdateAsync(T model);
        #endregion
        #region Delete
        void Delete(string id);
        Task DeleteAsync(string id);
       // void Delete(string text, string id,T model);
        void DeleteMany(Expression<Func<T, bool>> selector);
        Task DeleteManyAsync(Expression<Func<T, bool>> expression);
        #endregion


    }
}
