
using MongoDB.Driver;

namespace MongoRepository.Context
{
    public   interface IMongoContext
    {
         IMongoDatabase Database { get; }

    }
}
