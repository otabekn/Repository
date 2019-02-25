using Entity;
using LoggingRepository;
using MongoRepository.Context;
using RepositoryRule.Base;

namespace ServiceList
{

    public interface IDataService: IRepositoryBase<Data, string>
    {

    }
   public interface IEntityDataService:IRepositoryBase<EntityData, int>
    {

    }
    public class DataService : MongoRepository.MongoRepository<Data>, IDataService
    {
        public DataService(IMongoContext database) : base(database)
        {

        }
    }
}
