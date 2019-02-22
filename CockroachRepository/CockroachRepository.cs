using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CockroachRepository.Context;
using RepositoryRule.CacheRepository;
using RepositoryRule.Entity;
using SqlKata.Execution;

namespace CockroachRepository
{
    public class CockroachRepository<T> : ICockRoachRepository<T>
   where T : class, IEntity<int>
    {
        QueryFactory _db;
        string name;
        IChacheRepository<T> _cache;
        public CockroachRepository(ICockRoachContext cock)
        {

            _db=cock.Db;
            name = typeof(T).Name;
        }
        public CockroachRepository(ICockRoachContext cock, IChacheRepository<T> cache) : this(cock)
        {
            _cache =cache;
        }
       
        #region Add
        public void Add(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Add(model.Id.ToString(),model);
            _db.Query(name).Insert(model);

        }

        public async Task AddAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Add(model);
        }

        public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           foreach(var i in models)
            {
                Add(i);
            }
        }

        public async Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            AddRange(models);
        }
        #endregion
        #region Procedure
        public IEnumerable<T> CallProcedure(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> CallProcedure(string str)
        {
            throw new NotImplementedException();
        }

        public T CalProcedure(string functinname, object[] item, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Count
        public long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return 0;
        }

        public long Count(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return 0;
        }

        public long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Delete
        public void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache.Delete(model.Id.ToString());
            _db.Query(name).Where("Id", model.Id).Delete();
        }

        public async Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Delate(model);
        }

        public T Delete(int id)
        {
            _cache.Delete(id.ToString());
            _db.Query(name).Where("Id", id).Delete();
            return null;

        }
        //notReady
        //TODO Not Readt
        public void DeleteMany(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            
        }
        // TodoNot
        public async Task DeleteManyAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            DeleteMany(expression);
        }
        #endregion
        #region Find
        public IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
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

        public IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAsync(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAsync(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindReverse(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindReverse(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindReverse(string key, string value, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindReverseAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindReverseAsync(string key, string value, int offset, int limit)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Get
        public T Get(int id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(int id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public T GetFirst(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Update
        public void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateMany(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateManyAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
