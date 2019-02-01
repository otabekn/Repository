using System.Threading.Tasks;
using CacheManager.Core;
using RepositoryRule.CacheRepository;

namespace CacheRepository
{
    //change
    public class CacheRepository<T> : IChacheRepository<T>
        where T : class
    {
        string name;
        ICacheManager<T> _cache;
        CacheRepository()
        {
            name= typeof(T).Name;
        }
        
        public void Add(string id, T model)
        {
            Task.Run(() => { _cache.Add(id, model); });
        }

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

        public T Find(string id)
        {
            return _cache.Get(id);
        }

        public T FindFirst(string id)
        {
            return _cache.Get(id);
        }

        public async Task<T> FindFirstAsync(string id)
        {
            return _cache.Get(id);
        }

        public void Remove(string id)
        {
            Task.Run(() => { _cache.Remove(id); });
        }

        public void Update(string id, T modal)
        {
            Remove(id);
            Add(id, modal);
        }
    }
}
