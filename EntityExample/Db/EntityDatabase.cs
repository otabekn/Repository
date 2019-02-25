using Entity;
using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;


namespace EntityExample.Db
{
    public class EntityDatabase : DbContext, IDataContext
    {
        public EntityDatabase(DbContextOptions<EntityDatabase> options)
          : base(options)
        {
            Database.EnsureCreated();
        }
        public DbContext DataContext { get { return this; }  }
        public DbSet<EntityData> Entitys { get; set; }
    } 
}
