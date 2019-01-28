using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace RepositoryRule.EntityRepository
{
public    interface IEntityRepository<T>: IRepositoryBase<T, int>
     where T:class , IEntity<int>
    {
    }
}
