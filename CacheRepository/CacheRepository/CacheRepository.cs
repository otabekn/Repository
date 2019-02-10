using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using RepositoryRule.CacheRepository;

namespace CacheRepository
{
    //change
    public class CacheRepository<T> : IChacheRepository<T>
        where T : class
    {
        string name;

        CacheRepository()
        {
            name= typeof(T).Name;
        }
        #region Add
        public void Add(string id, T model)
        {
            Task.Run(() => {  });
        }

        public Task AddAsync(string id, T model)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<T> models)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<T> models)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Delete
        public void CacheDelete(string text, T model)
        {
            _cache.Get<T>("dfdf","dfd");
            
        }

        public void CatcheDelete(string text, string id, T model)
        {
            Task.Run(() => {
                _cache.Remove(id);
            });
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string text, string id, T model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany(Expression<Func<T, bool>> selector)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Find
        public T Find(string id)
        {
            return _cache.Get(id);
        }

        public T Find(Expression<Func<T, bool>> seletor)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public T FindFirst(string id)
        {
            return _cache.Get(id);
        }

        public T FindFirst(string field, string value)
        {
            throw new NotImplementedException();
        }

        public T FindFirst()
        {
            throw new NotImplementedException();
        }

        public T FindFirst(Expression<Func<T, bool>> selector)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindFirstAsync(string id)
        {
            
        }

        public Task<T> FindFirstAsync(string field, string value)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindFirstAsync(Expression<Func<T, bool>> selector)
        {
            throw new NotImplementedException();
        }

        public Task<T> FirstAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update

        public void Update(string id, T modal)
        {
            Delete(id);
            Add(id, modal);
        }

        public void Update(Expression<Func<T, T>> selector)
        {
            throw new NotImplementedException();
        }

        public Task Update(T model)
        {
            throw new NotImplementedException();
        }

        public void Update(List<T> models)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
