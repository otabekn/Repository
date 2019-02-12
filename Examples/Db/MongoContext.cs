using MongoDB.Driver;
using MongoRepository.Context;


namespace Examples.Db
{
    public class MongoContext : IMongoContext
    {
        public MongoContext()
        {
            var client = new MongoClient();
            Database=client.GetDatabase("johaExample");
            
        }
        public IMongoDatabase Database { get; }
    }
}
