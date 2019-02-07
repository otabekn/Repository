using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace SQLRepository.EntityRepository
{
    public interface IEntityRepository<T> : IRepositoryBase<T, int>
        where T : class, IEntity<int>
    {


    }
}
   
