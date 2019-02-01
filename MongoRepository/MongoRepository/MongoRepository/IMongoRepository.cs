using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace MongoRepository.MongoRepository
{
public    interface IMongoRepository<T>:IRepositoryBase<T, string>
        where T:class, IEntity<string>
    {
       
    }
}
