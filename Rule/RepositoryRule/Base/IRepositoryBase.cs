using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
  public  interface IRepositoryBase<T, TKey>
        where T:class, IEntity<TKey>
    {
        #region Sync Operations
        T FindFirst(TKey id);
        T FindLast(TKey id);
        void Add(T model);
        void Update(T model);
        void Delate(T model);
        #endregion
        #region  Async Operations
        Task<T> FindFirstAsync(TKey id);
        Task<T> FindLastAsync(TKey id);
        Task AddAsync(T model);
        Task UpdateAsync(T model);
        Task DelateAsync(T model);
        #endregion
        

    }
}
