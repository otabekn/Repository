using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    // this Repositoriy core you can update Add new Features and more
    // in  Realise this interface you can include cache but not use Find #region
    public interface IRepositoryBase<T, TKey>
        where T : class, IEntity<TKey>
    {
        //Get we can search Cache too
        #region Get
        /// <summary>
        /// Get Entity by id key
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        T Get(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Get with async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task<T> GetAsync(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Get First used searcg first cache if you add cache manager
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>        
        T GetFirst(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);

        /// <summary>
        /// simular but async
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion
        // add Entitys with cache
        #region Add
        /// <summary>
        /// Add Entity sync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        void Add(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Add Range entity Entity Async
        /// </summary>
        /// <param name="models"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Add Async module
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task AddAsync(T module, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Add Range Entity Async
        /// </summary>
        /// <param name="models"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task AddRangeAsync(List<T> models, [CallerLineNumber]int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion
        // Update Entitys with cache
        #region Update
        /// <summary>
        /// Update Entity ig you inclue cache update too
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Update Async update first Entity
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task UpdateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Update Many models
        /// </summary>
        /// <param name="models"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        void UpdateMany(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Update Many async models
        /// </summary>
        /// <param name="models"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task UpdateManyAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        //  void Update(Expression<Func<T, T>>selector,  [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        //Task UpdateAsync(Expression<Func<T, T>> selector,  [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);

        #endregion
       // Delete Entitys from database with cache
        #region Delate
        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="id"></param>
        T Delete(TKey id);
        /// <summary>
        /// Delete gived model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Delete async geved model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        ///
        void DeleteMany(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Delete many gived model
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task DeleteManyAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion
        // find model without cache 
        #region Find
        /// <summary>
        /// Find all Modules
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();
        /// <summary>
        /// Find section Gived function
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Find all gived function
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        //IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// find from database 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// Find by key "Property name"  
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value">value of property</param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        IEnumerable<T> FindReverse(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion
        #region FindAsync
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAsync(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAsync(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion
        //Get count from database
        #region Count
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        long Count(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="functinname"></param>
        /// <param name="item"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        T CalProcedure(string functinname, object[] item, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        IEnumerable<T> CallProcedure(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> CallProcedure(string str);
        #endregion
        // it is new Functionch
        #region New
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IEnumerable<T> FindReverse(int offset, int limit);
        /// <summary>
        /// Find Reverse 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindReverseAsync(int offset, int limit);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IEnumerable<T> FindReverse(string key, string value, int offset, int limit);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindReverseAsync(string key, string value, int offset, int limit);
        #endregion
    }
    public interface GroupGase
    {

    }
}
