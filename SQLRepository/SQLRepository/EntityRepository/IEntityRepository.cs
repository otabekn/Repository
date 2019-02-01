using RepositoryRule.Base;
using RepositoryRule.CacheRepository;
using RepositoryRule.Entity;

namespace SQLRepository.EntityRepository
{
    public interface IEntityRepository<T>:IRepositoryBase<T,int>
        where T :class, IEntity<int>
    {
        

    }
    public interface IEntityCache<T> : IChacheRepository<T>
        where T:class
    {


    }
}
