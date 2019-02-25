using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Operations.CompareExchange;
using Raven.Client.Documents.Queries;
using Raven.Client.Documents.Session;
using RepositoryRule.Base;
using RepositoryRule.CacheRepository;
using RepositoryRule.Entity;

namespace RavenRepository
{
    public interface IRavenDbStoreHolder
    {
        IDocumentStore Store { get; }
    }

    public class RavenDbRepository<T, TKey> : IRepositoryBase<T, TKey>, IDisposable
        where T : class, IEntity<TKey>

    {
        public IDocumentStore _store;
        IDocumentSession _session;
        ICacheRepository<T> _cache;
        public RavenDbRepository(IRavenDbStoreHolder context)
        {
            _store = context.Store;
            _store.Initialize();
            _session = _store.OpenSession();


        }
        public RavenDbRepository(IRavenDbStoreHolder context, ICacheRepository<T> cache)
        {
            _cache = cache;
        }
        #region Add

        public void Add(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Add(model.Id.ToString(), model);
            _session.Store(model);
            _session.SaveChanges();
        }
        public async Task AddAsync(T module, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Add(module);

        }

        public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.AddRange(models);
            foreach (var i in models)
            {
                _session.Store(i);
            }
            _session.SaveChanges();
        }

        public async Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            AddRange(models);

        }
        #endregion
        #region Procedure
        public IEnumerable<T> CallProcedure(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
          var r=  _store.Operations.Send(new PatchByQueryOperation(new IndexQuery {Query=str }));
            return JsonConvert.DeserializeObject<IEnumerable<T>>(r.WaitForCompletion().ToJson().ToString());
        }

        public Task<IEnumerable<T>> CallProcedure(string str)
        {
            return null;
        }

        public T CalProcedure(string functinname, object[] item, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Count
        public long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           return _session.Query<T>().CountLazily().Value
        }

        public long Count(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return _session.Query<T>().CountAsync(expression).Result;
        }

        public long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _session.Advanced.DocumentQuery<T>().Count(m=>m.GetType().GetProperty(field)==)
        }
        #endregion
        #region Delete
        public void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Delete(model.Id.ToString());
            _session.Delete<T>(model);

        }

        public async Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Delate(model);
        }

        public T Delete(TKey id)
        {
            _cache?.Delete(id.ToString());
            T item = null;
            if(id is string)
            {
              item=  _session.Query<T>().FirstOrDefaultAsync(m => m.Id.ToString() == id.ToString()).Result;
            }
            else if(id is int)
            {
               var s= Convert.ToInt32(id);
                item = _session.Query<T>().FirstOrDefaultAsync(m => Convert.ToInt32(m.Id) == s).Result;
            }
            _session.Delete(item);
            _session.SaveChanges();
            return item;
        }

        public void DeleteMany(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _store.Operations.Send(new DeleteByQueryOperation<T>("", expression));
        }

        public async Task DeleteManyAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            DeleteMany(expression);
            
        }
        #endregion

        public void Dispose()
        {
           
        }
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
        public T Get(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            
        }

        public Task<T> GetAsync(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public T GetFirst(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            T result = null;
            result = _cache?.FindFirst(expression);
            if (result != null) return result;
            return _session.Query<T>().FirstOrDefaultAsync(expression).Result;
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return GetFirst(expression);
        }
        #region 
        public void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _session.Advanced.update
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
    }
}
