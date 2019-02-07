
using Microsoft.EntityFrameworkCore;
using RepositoryRule.Base;

namespace SQLRepository.Context
{
    public interface IDataContext
    {
        DbContext DataContext { get; }
    }
    public interface IEntityFunction : IDataFunction
    {

    }
}
