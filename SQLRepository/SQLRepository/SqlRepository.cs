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
                _logger?.FinalyLog("Add", watch.ElapsedMilliseconds, model, caller, lineNumber);
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
            }
            catch (Exception ext)
            {

            }
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
                watch.Start();
                item= _cache?.Find( id.ToString());
                if(item!=null)
                {
                    return item;
                }
                item=_dbSet.Find(id);
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
            }
            catch (Exception ext)
            {
                //return null;
            }
            finally
            {

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


            }
            catch (Exception ext)
            {
                
            }
            finally
            {

            }
        }
        #endregion

        #region Find
        public IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return _dbSet.Where(keySelector);
        }

        public T FindFirst(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public long Count(string field, string value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
