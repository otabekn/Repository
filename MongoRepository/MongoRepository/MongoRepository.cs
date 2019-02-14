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
using MongoDB.Driver.Core.Operations;
using MongoDB.Driver.Core.Bindings;
using System.Threading;
using MongoDB.Bson.Serialization;

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
        public MongoRepository(IMongoContext context, IChacheRepository<T> cache):this(context)
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
            _cache?.AddAsync(model.Id, model);
             _db.InsertOneAsync(model);
            return Task.CompletedTask;
        }
        public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.AddRange(models);
            _db.InsertMany(models);
        }

        public Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.AddRangeAsync(models);
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
            _cache?.DeleteManyAsync(selector);
            _db.DeleteMany(selector);
            return Task.CompletedTask;
        }
        
        #endregion
        

        #region Find

        public IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return _db.Find(keySelector).ToEnumerable<T>();
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
        public T GetFirst(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _cache?.Find(selector);
            if (result != null)
            {
                return result;
            }
            return _db.Find(selector).FirstOrDefault();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _cache?.Find(selector);
            if (result != null)
            {
                return result;
            }
            return _db.Find(selector).FirstOrDefault();
        }
        #endregion

        #region Update
        public void UpdateMany(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Update(models);
            foreach(var i in models)
            {
                _db.FindOneAndReplace(m => m.Id == i.Id, i);
            }
        }

        public  Task UpdateManyAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Update(models);
            foreach (var i in models)
            {
                _db.FindOneAndReplace(m => m.Id == i.Id, i);
            }
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
        public long Count(Expression<Func<T, bool>> expression) => _db.Count(expression);
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
            var param = "(";
            foreach(var i in item)
            {
                param += "'" + Convert.ToString(i) + "'";
            }
            param += ")";
            var value= EvalAsync(functinname + param).Result;
            return BsonSerializer.Deserialize<T>(value.ToJson());
           
        }
        public IEnumerable<T> CallProcedure(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           
            var value = EvalAsync(str).Result;
            return BsonSerializer.Deserialize<IEnumerable<T>>(value.ToJson());

        }

        public async Task<BsonValue> EvalAsync(string text)
        {
            var client = _database as MongoClient;

            if (client == null)
                throw new ArgumentException("Client is not a MongoClient");

            var function = new BsonJavaScript(text);
            var op = new EvalOperation(_database.DatabaseNamespace, function, null);

            using (var writeBinding = new WritableServerBinding(client.Cluster, new CoreSessionHandle(NoCoreSession.Instance)))
            {
                return await op.ExecuteAsync(writeBinding, CancellationToken.None);
            }
        }

        public  async Task<IEnumerable<T>> CallProcedure(string str)
        {
                var result= await   EvalAsync(str);
            return BsonSerializer.Deserialize<IEnumerable<T>>(result.ToJson());
        }

        public T Delete(string id)
        {
           var element= _db.FindOneAndDelete(mbox => mbox.Id == id);
            return element;
        }

        public IEnumerable<T> FindAll()
        {
            return _db.Find(m=>true).ToList();
        }

        public IEnumerable<T> FindReverse(int offset, int limit)
        {

            return _db.Find(m => true).Sort(SortDescender()).Skip(offset).Limit(limit).ToList();

        }
        public SortDefinition<T> SortDescender(string id="_id") {
            return Builders<T>.Sort.Descending(id);
        }
        public IEnumerable<T> FindReverse(string key, string value, int  offset, int limit)
        {
            if (string.IsNullOrEmpty(key)) return null;
            var selector = new BsonDocument(key, value);
           return  _db.Find(selector).Sort(SortDescender()).Skip(offset).Limit(limit).ToList();

        }
        public  async Task<IEnumerable<T>> FindReverseAsync(string key, string value, int offset, int limit)
        {
            return FindReverse(key, value, offset, limit);
        }

        public Task<IEnumerable<T>> FindReverseAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
  
}
