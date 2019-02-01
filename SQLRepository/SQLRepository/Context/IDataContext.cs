
using Microsoft.EntityFrameworkCore;
namespace SQLRepository.Context
{
    public interface IDataContext
    {
        DbContext DataContext { get; }
    }
}
