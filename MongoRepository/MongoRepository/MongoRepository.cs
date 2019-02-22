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
        //in this region we can add Entitys
        #region Add 

        /// <summary>
        /// Adding module if you include cache add cache
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>

        public void Add(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Add(model.Id, model);
            _db.InsertOne(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public Task AddAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.AddAsync(model.Id, model);
             _db.InsertOneAsync(model);
            return Task.CompletedTask;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="models"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.AddRange(models);
            _db.InsertMany(models);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="models"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>

        public Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.AddRangeAsync(models);
            _db.InsertManyAsync(models);
            return Task.CompletedTask;

        }

        #endregion
        
        #region Delate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Delete(model.Id);
            _db.DeleteOne(m => m.Id== model.Id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Delete(model.Id);
            _db.DeleteOneAsync(m => m.Id == model.Id);
            return Task.CompletedTask;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void DeleteMany(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
             _cache?.DeleteMany(selector);
            _db.DeleteMany(selector);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public Task DeleteManyAsync(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.DeleteManyAsync(selector);
            _db.DeleteMany(selector);
            return Task.CompletedTask;
        }
        
        #endregion
        

        #region Find
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return _db.Find(keySelector).ToEnumerable<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
         {
           return _db.Find(selector).Skip(offset).Limit(limit).ToEnumerable();
         }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
                var document = new BsonDocument(field, value);
                return _db.Find(document).ToEnumerable();
        }
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
        public IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
                var document = new BsonDocument(field, value);
                return _db.Find(document).Skip(offset).Limit(limit).ToEnumerable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public IEnumerable<T> FindReverse(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result=_db.Find(selector).ToList();
            result.Reverse();
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return _db.Find(selector).ToList();
        }     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return await _db.Find(selector).Skip(offset).Limit(limit).ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAsync(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var selector = new BsonDocument(field, value);
            return await _db.Find(selector).ToListAsync();
        }
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
        public async Task<IEnumerable<T>> FindAsync(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var selector = new BsonDocument(field, value);
            return await _db.Find(selector).Skip(offset).Limit(limit).ToListAsync();
        }

        #endregion
        
        #region Get
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public T Get(string id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _cache?.Find(id);
            if (result != null)
            {
                return result;
            }
            return _db.Find(m=>m.Id== id).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _cache?.Find(id);
            if (result != null)
            {
                return result;
            }
            return _db.Find(m => m.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public T GetFirst(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var result = _cache?.Find(selector);
            if (result != null)
            {
                return result;
            }
            return _db.Find(selector).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="models"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void UpdateMany(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Update(models);
            foreach(var i in models)
            {
                _db.FindOneAndReplace(m => m.Id == i.Id, i);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="models"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public  Task UpdateManyAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Update(models);
            foreach (var i in models)
            {
                _db.FindOneAndReplace(m => m.Id == i.Id, i);
            }
            return Task.CompletedTask;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="model"></param>
       /// <param name="lineNumber"></param>
       /// <param name="caller"></param>
        public void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.Update(model);
            _db.FindOneAndReplace(m=>m.Id == model.Id, model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public Task UpdateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _cache?.UpdateAsync(model);
            _db.FindOneAndReplace(m => m.Id == m.Id, model);
            return Task.CompletedTask;
        }
        #endregion

        #region  Count
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public long Count(Expression<Func<T, bool>> expression) => _db.Count(expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Count(string field, string value)
        {
            var document = new BsonDocument(field, value);
            return _db.Count(filter: document);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
          return  _db.Count(mbox=>true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public long Count(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
          return  _db.Count(selector);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            var data = new BsonDocument(field, value);
            return _db.Count(data);
        }
        #endregion
        
        #region Procedure
        /// <summary>
        /// 
        /// </summary>
        /// <param name="functinname"></param>
        /// <param name="item"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public IEnumerable<T> CallProcedure(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
           
            var value = EvalAsync(str).Result;
            return BsonSerializer.Deserialize<IEnumerable<T>>(value.ToJson());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public  async Task<IEnumerable<T>> CallProcedure(string str)
        {
                var result= await   EvalAsync(str);
            return BsonSerializer.Deserialize<IEnumerable<T>>(result.ToJson());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Delete(string id)
        {
           var element= _db.FindOneAndDelete(mbox => mbox.Id == id);
            return element;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> FindAll()
        {
            return _db.Find(m=>true).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IEnumerable<T> FindReverse(int offset, int limit)
        {

            return _db.Find(m => true).Sort(SortDescender()).Skip(offset).Limit(limit).ToList();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SortDefinition<T> SortDescender(string id="_id") {
            return Builders<T>.Sort.Descending(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IEnumerable<T> FindReverse(string key, string value, int  offset, int limit)
        {
            if (string.IsNullOrEmpty(key)) return null;
            var selector = new BsonDocument(key, value);
           return  _db.Find(selector).Sort(SortDescender()).Skip(offset).Limit(limit).ToList();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public  async Task<IEnumerable<T>> FindReverseAsync(string key, string value, int offset, int limit)
        {
            return FindReverse(key, value, offset, limit);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> FindReverseAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
  
}
