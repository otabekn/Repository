using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoRepository.Context;
using MongoRepository.MongoRepository;
using RepositoryRule.Entity;
using RepositoryRule.LoggerRepository;
using System.Collections.Generic;
using MongoDB.Bson;
using RepositoryRule.CacheRepository;

namespace MongoRepository
{
    //change
    public class MongoRepository<T> : IMongoRepository<T>
        where T : class, IEntity<string>
    {
        #region Header
        IMongoCollection<T> _db;
        protected IMongoDatabase _database;
        ILoggerRepository _logger;
        IChacheRepository<T> _cache;
        string name;
        public MongoRepository(IMongoContext database)
        {
            _database= database.Database;
            name=typeof(T).Name;
            
            _db = _database.GetCollection<T>(name);
        }
        public MongoRepository(IMongoContext context, ILoggerRepository logger):this(context)
        {
            _logger = logger;
        }
        public MongoRepository(IMongoContext context, ILoggerRepository logger, IChacheRepository<T> cache):this(context, logger)
        {
            _cache = cache;
        }
        public MongoRepository(IMongoContext context, IChacheRepository<T> cache) : this(context)
        {
            _cache = cache;
        }
        #endregion

        #region Add 
        public void Add(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Add(model.Id, model);
            _db.InsertOne(model);
        }

        public Task AddAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache.AddAsync(model.Id, model);
             _db.InsertOneAsync(model);
            return Task.CompletedTask;
        }
        public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache.AddRange(models);
            _db.InsertMany(models);
        }

        public Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache.AddRangeAsync(models);
            _db.InsertManyAsync(models);
            return Task.CompletedTask;

        }

        #endregion
        
        #region Delate
        public void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Delete(model.Id);
            _db.DeleteOne(m => m.Id== model.Id);
        }
        public Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Delete(model.Id);
            _db.DeleteOneAsync(m => m.Id == model.Id);
            return Task.CompletedTask;
        }
        public void DeleteMany(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
             _cache?.DeleteMany(selector);
            _db.DeleteMany(selector);

        }
        public Task DeleteManyAsync(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache.DeleteManyAsync(selector);
            _cache.DeleteMany(selector);
            return Task.CompletedTask;
        }
        
        #endregion
        
        #region Find

        public IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {

                return _db.Find(keySelector).ToEnumerable<T>();
            }
            catch(Exception ext)
            {
                return null;
            }
        }

        public T FindFirst(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           var result= _cache?.Find(selector);
            if (result != null)
            {
                return result;
            }
            return _db.Find(selector).FirstOrDefault();
        }
        public async Task<T> FindFirstAsync(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _cache?.Find(selector);
            if (result != null)
            {
                return result;
            }
            return _db.Find(selector).FirstOrDefault();

        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
         {
           return _db.Find(selector).Skip(offset).Limit(limit).ToEnumerable();
         }
        public IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
                var document = new BsonDocument(field, value);
                return _db.Find(document).ToEnumerable();
         }
        public IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
                var document = new BsonDocument(field, value);
                return _db.Find(document).Skip(offset).Limit(limit).ToEnumerable();
        }
        public IEnumerable<T> FindReverse(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result=_db.Find(selector).ToList();
            result.Reverse();
            return result;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return _db.Find(selector).ToList();
        }

       

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return await _db.Find(selector).Skip(offset).Limit(limit).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var selector = new BsonDocument(field, value);
            return await _db.Find(selector).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var selector = new BsonDocument(field, value);
            return await _db.Find(selector).Skip(offset).Limit(limit).ToListAsync();
        }

        #endregion
        
        #region Get

        public T Get(string id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _cache?.Find(id);
            if (result != null)
            {
                return result;
            }
            return _db.Find(m=>m.Id== id).FirstOrDefault();
        }

        public async Task<T> GetAsync(string id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _cache?.Find(id);
            if (result != null)
            {
                return result;
            }
            return _db.Find(m => m.Id == id).FirstOrDefault();
        }
        #endregion
        
        #region Update
        public void UpdateMany(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache.Update(models);
            foreach(var i in models)
            {
                _db.FindOneAndReplace(m => m.Id == i.Id, i);
            }
        }

        public  Task UpdateManyAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache.Update(models);
            foreach (var i in models)
            {
                _db.FindOneAndReplace(m => m.Id == i.Id, i);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// TODO :Not Relise
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void Update(Expression<Func<T, T>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           // _db.UpdateMany(selector);
        }
        /// <summary>
        /// TODO :not Relise
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public Task UpdateAsync(Expression<Func<T, T>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return Task.CompletedTask;
        }

        public void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Update(model);
            _db.FindOneAndReplace(m=>m.Id == model.Id, model);
        }

        public Task UpdateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.UpdateAsync(model);
            _db.FindOneAndReplace(m => m.Id == m.Id, model);
            return Task.CompletedTask;
        }
        #endregion
        
        #region  Count
        public long Count(Expression<Func<T, bool>> expression) => _data.Count(expression);
        public long Count(string field, string value)
        {
            var document = new BsonDocument(field, value);
            return _db.Count(filter: document);
        }
        public long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
          return  _db.Count(mbox=>true);
        }

        public long Count(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
          return  _db.Count(selector);
        }

        public long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var data = new BsonDocument(field, value);
            return _db.Count(data);
        }
        #endregion
        
        #region Procedure
        public T CalProcedure(string functinname, object[] item, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<T> CallProcedure(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
  
}
