using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DapperRepository
{
    /// <summary>
    /// The repository interface.
    /// </summary>
    /// <typeparam name="T">The domain entity</typeparam>
    public interface IRepository<T> where T : EntityBase
    {
        T Insert(T item);
        bool Delete(int id);
        bool Update(T item);
        T GetByID(int id);
        int GetCount();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
    }
}