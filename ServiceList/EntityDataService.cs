using Entity;
using EntityRepository;
using EntityRepository.Context;

namespace ServiceList
{
    public class EntityDataService : SqlRepository<EntityData>, IEntityDataService
    {
        public EntityDataService(IDataContext context) : base(context)
        {
        }
    }
}
