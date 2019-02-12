using MongoDB.Bson.Serialization.Attributes;
using RepositoryRule.Entity;

namespace Entity
{
  public class Data : IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
