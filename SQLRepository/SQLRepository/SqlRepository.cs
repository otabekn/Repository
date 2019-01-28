
using System.Threading.Tasks;
using RepositoryRule.Base;
using RepositoryRule.CacheRepository;
using RepositoryRule.Entity;

namespace SQLRepository
{
public partial class SqlRepository<T>:IRepositoryBase<T, int>
        where T:class, IEntity<int>
    {
        public SqlRepository()
        {

        }
        public SqlRepository(IChacheRepository<T,string> cache)
        {

        }

        public void Add(T model)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(T model)
        {
            throw new System.NotImplementedException();
        }

        public void Delate(T model)
        {
            throw new System.NotImplementedException();
        }

        public Task DelateAsync(T model)
        {
            throw new System.NotImplementedException();
        }

        public T FindFirst(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> FindFirstAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public T FindLast(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> FindLastAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T model)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(T model)
        {
            throw new System.NotImplementedException();
        }
    }
}
