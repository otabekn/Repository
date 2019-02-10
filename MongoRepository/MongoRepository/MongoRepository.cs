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

namespace MongoRepository
{
    //change
    public class MongoRepository<T> : IMongoRepository<T>
        where T : class, IEntity<string>
    {
        IMongoCollection<T> _data;
        protected IMongoDatabase _db;
        ILoggerRepository _logger;

        string name;
        public MongoRepository(IMongoContext database)
        {
            _db = database.Database;
            name=typeof(T).Name;
            _data = _db.GetCollection<T>(name);
        }
        public MongoRepository(IMongoContext context, ILoggerRepository logger):this(context)
        {
            _logger = logger;
        }

        #region Add 
        public void Add(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region Delate
        public void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }
        public void DeleteMany(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            _data.DeleteMany(expression);
        }
        
        
        #endregion

        #region Find

        public IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                return _data.Find(keySelector).ToEnumerable<T>();
            }
            catch(Exception ext)
            {
                return null;
            }
        }
        public T FindFirst(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return _data.Find(expression).FirstOrDefault();
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
         {
            try
            {
                return _data.Find(selector).Skip(offset).Limit(limit).ToEnumerable();
            }
            catch(Exception ext)
            {
                return null;
            }
         }
        public IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                var document = new BsonDocument(field, value);
                return _data.Find(document).ToEnumerable();
            }
            catch(Exception ext)
            {
                return null;
            }
            
        }
        public IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                var document = new BsonDocument(field, value);
                return _data.Find(document).Skip(offset).Limit(limit).ToEnumerable();
            }
            catch
            {
                return null;
            }
            
        }
                
        #endregion
        #region Get

        public T Get(string id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(string id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
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
        #endregion
        #region  Count
        public long Count(Expression<Func<T, bool>> expression) => _data.Count(expression);
        public long Count(string field, string value)
        {
            var ss=_data.Distinct(name => name.Id == "");
            var document = new BsonDocument(field, value);
            return _data.Count(filter: document);
        }

        public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
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

        public void Update(Expression<Func<T, T>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Expression<Func<T, T>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindReverse(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindFirstAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
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

        public long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

        public long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            throw new NotImplementedException();
        }

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
