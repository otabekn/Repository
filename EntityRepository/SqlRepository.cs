using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using RepositoryRule.CacheRepository;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace EntityRepository
{
    public class SqlRepository<T> : IEntityRepository<T>
         where T : class, IEntity<int>
    {
        #region Head
        IChacheRepository<T> _cache;
        protected DbContext _db;
        protected DbSet<T> _dbSet;
        private string name;
        public SqlRepository(IDataContext context)
        {
            _db = context.DataContext;
            _dbSet = context.DataContext.Set<T>();
            name = typeof(T).Name;

        }
        public SqlRepository(IDataContext context,IChacheRepository<T> cache) : this(context)
        {
            _cache = cache;
        }
        #endregion

        #region Create
        public void Add(T model, [CallerLineNumber]int lineNumber = 0, [CallerMemberName] string caller = null)
        {
                 _cache?.Add(name + model.Id.ToString(), model);
                _dbSet.Add(model);                
        }
        public async Task AddAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           _cache?.Add(model.Id.ToString(), model);
           await _dbSet.AddAsync(model);
           await _db.SaveChangesAsync();
        }
        public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.AddRange(models);
            _dbSet.AddRange(models);
            _db.SaveChangesAsync();              
        }
        public Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.AddRange(models);
            _dbSet.AddRangeAsync(models);
            _db.SaveChangesAsync();               
            return Task.CompletedTask;
        }
        #endregion
        
        #region Get
        public async Task<T> GetAsync(int id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            T item = null;
            item = await _cache?.FindFirstAsync(id.ToString());
            if (item == null)
            {
                return item;
            }
            item = await _dbSet.FindAsync(id);
            return item;
        }
        public T Get(int id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            T item = null;
            item = _cache?.Find(id.ToString());
            if (item != null)
            {
                return item;
            }
            item = _dbSet.Find(id);
            return item;
        }
        public T GetFirst(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           var result= _cache?.FindFirst(selector);
            if (result != null) return result;
            result = _dbSet.FirstOrDefault(selector);
            return result;
        }
        public Task<T> GetFirstAsync(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result=_cache?.FindFirstAsync(selector);
            result= _dbSet.FirstOrDefaultAsync(selector);
            return result;
        }
        #endregion
        
        #region Update
        public void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Update(model.Id.ToString(), model);
            _dbSet.Update(model);
            _db.SaveChanges();
        }
        public async Task UpdateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           _cache?.Update(model.Id.ToString(), model);
           _dbSet.Update(model);
           await _db.SaveChangesAsync();
        }
        public void UpdateMany(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Update(models);
            _dbSet.UpdateRange(models);
            _db.SaveChanges();
        }
        public Task UpdateManyAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Update(models);
            _dbSet.UpdateRange(models);
            _db.SaveChangesAsync();
            return Task.CompletedTask;
        }
        public void Update(Expression<Func<T, T>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           _cache.Update(selector);
           _dbSet.Update(selector);
           _db.SaveChanges();
        }
        public Task UpdateAsync(Expression<Func<T, T>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache.Update(selector);
            _dbSet.Update(selector);
            _db.SaveChanges();
            return Task.CompletedTask;
        }

        #endregion
        
        #region Delate
        public void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
                 _cache?.Delete(model.Id.ToString());
                _dbSet.Remove(model);
            _db.SaveChanges();
             
        }
        public async Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
                 _dbSet.Remove(model);
                _cache?.Delete(model.Id.ToString());
                await _db.SaveChangesAsync();
        }
        public async Task DeleteManyAsync(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache.DeleteMany(selector);
            await _dbSet.Where(selector).DeleteAsync();
            _db.SaveChanges();
        }
        public void DeleteMany(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.DeleteMany(selector);
            _dbSet.Where(selector).Delete();
            _db.SaveChanges();
        }
        #endregion
        
        #region Find
        public IEnumerable<T> Find(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
               var result = _dbSet.Where(selector);
               return result;
        }
       
        public IEnumerable<T> FindReverse(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
               var result = _dbSet.Where(selector).Reverse();
                 return result;
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           var result = _dbSet.Where(selector).SkipLast(offset).TakeLast(limit);
           return result;
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           var result = _dbSet.Where(selector);
           return result;       
           
        }
     
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           var result = _dbSet.Where(selector);
           return result;
        }
        public async Task<IEnumerable<T>> FindAsync(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
          var props = typeof(T).GetProperty(field);
          var result = _dbSet.Where(m => props.GetValue(m, null) == value);
          return result;           
           
        }
        public async Task<IEnumerable<T>> FindAsync(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
                var props = typeof(T).GetProperty(field);
                var result = _dbSet.Where(m => props.GetValue(m, null) == value);
                return result;
        }
        //change
        public IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
          var props = typeof(T).GetProperty(field);
          var result = _dbSet.Where(m => props.GetValue(m, null) == value);
          return result;
           
        }
        public IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           var props = typeof(T).GetProperty(field);
           var result = _dbSet.Where(m => props.GetValue(m, null) == value).Skip(offset).Take(limit);
           return result;
           
        }
        #endregion
        
        #region Count
        public long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           var result = _dbSet.Count();
           return result;
            
        }
        public long Count(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           var result = _dbSet.Count(expression);
           return result;           
        }

        public long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
          var props = typeof(T).GetProperty(field);
          var result = _dbSet.Count(m => props.GetValue(m, null) == value);
          return result;
        }
        #endregion 
   
        #region Procedure change
        public T CalProcedure(string functinname, object[] item, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _dbSet.FromSql(functinname, item).FirstOrDefault();
            return result;
        }
        public IEnumerable<T> CallProcedure(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _dbSet.FromSql(str);
            return result;
        }
        public async Task<IEnumerable<T>> CallProcedure(string str)
        {
            var result =  _dbSet.FromSql(str);
            return result;
        }
        #endregion
    }
}
