using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Dapper;

namespace DapperRepository
{
    public abstract class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly string _tableName;

        
        public IDbConnection Connection;  // return new NpgsqlConnection(ConnectionHelper.ConnectionString); from start page

        public Repository(IDbConnection _connection)
        {
            Connection = _connection;
        }
        public Repository(string tableName)
        {
            _tableName = tableName;
        }

        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        public virtual T Insert(T item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                 item.id = cn.Insert<int>(_tableName, parameters);
            }
            return item;
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual bool Update(T item)
        {

            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                try
                {
                    cn.Update(_tableName, parameters);
                    // cn.Update(item);    ==> Dapper.Contrib.Extensions
                }
                catch (Exception )
                {
                    return false;
                }
                return true;

            }
        }

        public virtual bool Delete(int id)
        {
            using (IDbConnection cn = Connection)
            {
                try
                {
                    cn.Open();
                    cn.Execute("DELETE FROM " + _tableName + " WHERE id=@Id", new { Id = id });
                }
                catch (Exception)
                {

                    return false;
                }
                return true;
            }
        }

        public virtual T GetByID(int id)
        {
            T item = default(T);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                item = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE id=@Id", new { Id = id }).SingleOrDefault();
            }

            return item;
        }

        public virtual int GetCount()
        {
            int count = 0;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                count = cn.Query<int>("SELECT count(*) FROM " + _tableName).FirstOrDefault();
            }

            return count;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> items = null;

            // extract the dynamic sql query and parameters from predicate
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>(result.Sql, (object)result.Param);
            }

            return items;
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>All items</returns>
        public virtual IEnumerable<T> GetAll()
        {
            IEnumerable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName);
            }

            return items;
        }
    }
}