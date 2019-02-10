
using Microsoft.EntityFrameworkCore;
using RepositoryRule.Base;

namespace EntityRepository.Context
{
    public interface IDataContext
    {
        DbContext DataContext { get; }
    }
    public interface IEntityFunction : IDataFunction
    {

    }
}
