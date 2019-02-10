using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace EntityRepository.Context
{
    public interface IEntityRepository<T> : IRepositoryBase<T, int>
       where T : class, IEntity<int>
    {


    }
}
