using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public  interface IRepositoryBase<T, TKey>
        where T:class, IEntity<TKey>
    {

        #region Get
        T Get(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task<T> GetAsync(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion

        #region Add
        void Add(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task AddAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task AddRangeAsync(List<T> models, [CallerLineNumber]int lineNumber=0, [CallerMemberName] string caller=null);
        #endregion

        #region Update
        void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task UpdateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void UpdateMany(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task UpdateManyAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Update(Expression<Func<T, T>>selector,  [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task UpdateAsync(Expression<Func<T, T>> selector,  [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);

        #endregion

        #region Delate
        void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void DeleteMany(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task DeleteManyAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion

        #region Find
        IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        T FindFirst(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        IEnumerable<T> FindReverse(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        #endregion
        #region FindAsync
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task<IEnumerable<T>> FindAsync(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        Task<IEnumerable<T>> FindAsync(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion

        #region Count
        long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        long Count(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion

        #region

        #endregion




    }
}
