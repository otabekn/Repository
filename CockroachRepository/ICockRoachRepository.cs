using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace CockroachRepository
{
    public interface ICockRoachRepository<T> : IRepositoryBase<T, int>
          where T : class, IEntity<int>
    {
    }  
}