using RepositoryRule.Base;
using RepositoryRule.Entity;
namespace RepositoryRule.CacheRepository
{
    public interface IChacheRepository<T, TKey>: IRepositoryBase<T, TKey>
        where T:class, IEntity<TKey>
    {
    }
}
