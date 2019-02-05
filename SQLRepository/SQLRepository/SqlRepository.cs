using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryRule.CacheRepository;
using RepositoryRule.Entity;
using RepositoryRule.LoggerRepository;
using SQLRepository.Context;
using SQLRepository.EntityRepository;
using Z.EntityFramework.Plus;

namespace SQLRepository
{
    //change
public partial class SqlRepository<T>: IEntityRepository<T>
        where T:class, IEntity<int>
    {
        ILoggerRepository _logger;
        IChacheRepository<T> _cache;
        protected DbContext _db;
        protected DbSet<T> _dbSet;
        private string name;
        public SqlRepository(IDataContext context)
        {
            _db = context.DataContext;
            _dbSet = context.DataContext.Set<T>();
            name=typeof(T).Name;
            
        }
        public SqlRepository(IDataContext context, ILoggerRepository logger):this(context) {
            _logger = logger;
        }
        public SqlRepository(IDataContext context, ILoggerRepository logger, IChacheRepository<T> cache):this(context, logger)
        {
            _cache = cache;
        }
        public SqlRepository(IDataContext context, IChacheRepository<T> cache):this(context)
        {
            _cache = cache;
        }


        #region Create
        public void Add(T model, [CallerLineNumber]int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                _cache?.Add(name+model.Id.ToString(), model);
                _dbSet.Add(model);
                watch.Stop();
                Logging("Add modal",watch.ElapsedMilliseconds, lineNumber, caller, model);
            }
            catch(Exception ext)
            {
                _cache?.CatcheDelete("Add Exeptions",model.Id.ToString(), model);
                watch.Stop();
                _logger?.CatchError("Add Error",watch.ElapsedMilliseconds,model, ext, caller, lineNumber);
            }                        
        }
        public async Task AddAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                await _dbSet.AddAsync(model);
                _cache?.Add( model.Id.ToString(), model);
                watch.Stop();
                Logging("Add async", watch.ElapsedMilliseconds, lineNumber, caller, model);
            }
            catch (Exception ext)
            {

            }
        }
        public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                _dbSet.AddRange(models);
                _db.SaveChangesAsync();
                watch.Stop();
                Logging("AddRange", watch.ElapsedMilliseconds, lineNumber, caller, models);
            }
            catch (Exception ext)
            {

            }
        }

        public Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                _dbSet.AddRangeAsync(models);
                _db.SaveChangesAsync();
                watch.Stop();
                Logging("AddRangeAsync", watch.ElapsedMilliseconds, lineNumber, caller, models);

            }
            catch (Exception ext)
            {

            }
            return Task.CompletedTask;
        }

        //public async T
        #endregion

        #region Read
        public async Task<T> GetAsync(int id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                T item = null;
                item = await _cache?.FindFirstAsync( id.ToString());
                if (item == null)
                {
                    return item;
                }
                item = await _dbSet.FindAsync(id);
                watch.Stop();

                Logging("Get",watch.ElapsedMilliseconds, lineNumber,caller, id);


                return item;
            }
            catch (Exception ext)
            {
                return null;
            }
        }
        public T Get(int id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                T item = null;
                
                item= _cache?.Find( id.ToString());
                if(item!=null)
                {
                    return item;
                }
                item=_dbSet.Find(id);
                watch.Stop();
                Logging("Get", watch.ElapsedMilliseconds, lineNumber, caller, id);
                return item;
            }
            catch(Exception ext)
            {
                return null;
            }
            
        }
        #endregion

        #region Update
        public void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {

                watch.Start();
                _cache?.Update( model.Id.ToString(), model);
                _dbSet.Update(model);
                watch.Stop();
                Logging("Update", watch.ElapsedMilliseconds, lineNumber, caller, model);
            }
            catch (Exception ext)
            {
             
            }
        }
        public async Task UpdateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                _cache?.Update( model.Id.ToString(), model);
                _dbSet.Update(model);
                await _db.SaveChangesAsync();
                watch.Stop();
                Logging("Update Async", watch.ElapsedMilliseconds, lineNumber, caller, model);
            }
            catch (Exception ext)
            {
                //return null;
            }
        }
        #endregion

        #region Delate
        public void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                _cache?.Remove( model.Id.ToString());
                 _dbSet.Remove(model);
                watch.Stop();
                Logging("Delete", watch.ElapsedMilliseconds, lineNumber, caller, model);
            }
            catch (Exception ext)
            {
                
            }
        }
        public async Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                _dbSet.Remove(model);
                _cache?.Remove(model.Id.ToString());
                await _db.SaveChangesAsync();
                watch.Stop();

                Logging("Delete Async", watch.ElapsedMilliseconds, lineNumber, caller, model);
            }
            catch (Exception ext)
            {
                
            }
           
        }
        #endregion

        #region Find
        public IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                var result= _dbSet.Where(keySelector);
                watch.Stop();
                return result; 

            }
            catch(Exception ext)
            {
                return null;
            }
            
        }

        public T FindFirst(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                var result=_dbSet.FirstOrDefault(expression);
                watch.Stop();
                Logging("Find", watch.ElapsedMilliseconds, lineNumber, caller, result);
                return result;
            }
            catch (Exception ext)
            {
                return null;
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                var result=_dbSet.Where(selector);
                watch.Stop();
                return result;
            }
            catch (Exception ext)
            {
                return null;
            }
        }
        //change
        public IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                var props=typeof(T).GetProperty(field);
                var result=_dbSet.Where(m => props.GetValue(m, null) == value);
                watch.Stop();
                Logging("Find", watch.ElapsedMilliseconds, lineNumber, caller,field+ ":"+value);
                return result;
            }
            catch (Exception ext)
            {
                return null;
            }
        }

        public IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                var props = typeof(T).GetProperty(field);
                var result = _dbSet.Where(m => props.GetValue(m, null) == value).Skip(offset).Take(limit); 
                watch.Stop();
                Logging("Find", watch.ElapsedMilliseconds, lineNumber, caller, field + ":" + value);
                return result;
            }
            catch (Exception ext)
            {
                return null;
            }
        }

        public void DeleteMany(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                _dbSet.Where(expression).Delete();
                watch.Stop();
                Logging("DeleteMany", watch.ElapsedMilliseconds, lineNumber, caller, expression);
            }
            catch (Exception ext)
            {
              
            }
        }
        public long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                var result=_dbSet.Count();
                watch.Stop();
                Logging("Count", watch.ElapsedMilliseconds, lineNumber, caller, null);
                return result;
            }
            catch(Exception ext)
            {
                return 0;
            }
        }
        public long Count(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                var result=_dbSet.Count(expression);

                watch.Stop();
                Logging("Count", watch.ElapsedMilliseconds, lineNumber, caller, expression) ;
                return result;
            }
            catch (Exception ext)
            {
                return 0;
            }
        }

        public long Count(string field, string value)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                var props = typeof(T).GetProperty(field);
                var result = _dbSet.Count(m => props.GetValue(m, null) == value);

                watch.Stop();
                Logging("Count", watch.ElapsedMilliseconds, 0, "", null);
                return result;
            }
            catch (Exception ext)
            {
                return 0;
            }
        }

         #endregion
        #region Loggin
        
        public void Logging(string text, long msec, int lineNumber, string caller, object model )
        {
            _logger?.FinalyLog(text, msec,model, caller, lineNumber);
        }   
        public void ErrorLogging()
        {

        }
        #endregion
    }
}
