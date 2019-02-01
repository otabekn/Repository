using System.Threading.Tasks;
using RepositoryRule.Base;
using RepositoryRule.Entity;
namespace RepositoryRule.CacheRepository
{
    public interface IChacheRepository<T>
        where T:class
    
    {
        T Find(string id);
        void Update(string id, T modal);
        void Remove(string id);
        T FindFirst(string id);
        Task<T> FindFirstAsync(string id);
        void Add(string id,T model);
        void CatcheDelete(string text, string id,
            T model);
    }
}
