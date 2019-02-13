using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EasyCaching.Core;
using RepositoryRule.CacheRepository;

namespace CacheRepository
{
    public static class CacheConfig
    {
        public static Dictionary<string, TimeSpan> Entitys { get; set; }
    }

    public class CacheRepository<T> : IChacheRepository<T>
        where T : class
    {
        string name;
        TimeSpan _time;
        private readonly IEasyCachingProvider _provider;
        CacheRepository(IEasyCachingProvider provider)
        {
            name= typeof(T).Name;
            _provider = provider;
        }
        #region Add
        public void Add(string id, T model)
        {
            _provider.Set(id, model,_time);
        }
        public Task AddAsync(string id, T model)
        {
            _provider.SetAsync(id, model, _time);
            return Task.CompletedTask;
        }

        public void AddRange(List<T> models)
        {
            foreach(var i in models)
            {
                var f= i.GetType().GetProperty("Id");
                _provider.Set(Convert.ToString(f.GetConstantValue()), i, _time);
            }
        }

        public Task AddRangeAsync(List<T> models)
        {
            return Task.Run(() =>
            {
                foreach (var i in models)
                {
                    var f = i.GetType().GetProperty("Id");
                    _provider.Set(Convert.ToString(f.GetConstantValue()), i, _time);
                }
            });
        }
        #endregion
        #region Delete
        //public void CacheDelete(string id, T model)
        //{
        //    _provider.Remove(id);
            
        //}

        //public void CatcheDelete(string text, string id, T model)
        //{
            
        //}

        public void Delete(string id)
        {
            _provider.Remove(id);
        }

        //public void Delete(string text, string id, T model)
        //{
        //    _provider
        //}

        public Task DeleteAsync(string id)
        {
            _provider.Remove(id);
            return Task.CompletedTask;
        }
        //change
        public void DeleteMany(Expression<Func<T, bool>> selector)
        {
            //_provider.Get()
        }
        //change
        public Task DeleteManyAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Find
        public T Find(string id)
        {
            _provider.
        //    return _cache.Get(id);
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
