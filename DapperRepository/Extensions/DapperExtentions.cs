﻿using System.Data;
using Dapper;
using System.Linq;
using System.Collections.Generic;

namespace DapperRepository
{
    public static class DapperExtensions
    {
        
        public static T Insert<T>(this IDbConnection cnn, string tableName, dynamic param)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param), param);
            return result.First();
        }
        
        public static void Update(this IDbConnection cnn, string tableName, dynamic param)
        {
            SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param), param);
        }
        
    }
}