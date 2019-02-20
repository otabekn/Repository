using CockroachRepository;
using CockroachRepository.Context;
using Npgsql;
using RepositoryRule.Entity;
using SqlKata.Execution;
using System;

namespace CockroachDb
{
    class Program
    {
        static void Main(string[] args)
        {
            CockContext context = new CockContext();

            IDataService service = new DataService(context);

            service.Add(new Data() { Id = 0, Name = "joha" });
        }

    }
    public class CockContext : ICockRoachContext
    {
        public CockContext()
        {
            var connStringBuilder = new NpgsqlConnectionStringBuilder();

            connStringBuilder.Host = "localhost";
            connStringBuilder.Port = 26257;
            connStringBuilder.Database = "JOHA";
            //postgres://localhost:26257/JOHA
            var ff = new NpgsqlConnection("postgres://localhost:26257/JOHA");// connStringBuilder.ConnectionString+ "?sslmode=disable");
            ff.Open();
            
            var compiler=new SqlKata.Compilers.PostgresCompiler();
            Db=new QueryFactory(ff, compiler);
        }
        public QueryFactory Db { get; }
    }

    public class DataService : CockroachRepository<Data>, IDataService
    {
        public DataService(ICockRoachContext cock) : base(cock)
        {
        }
    }
    public interface IDataService: ICockRoachRepository<Data>
    {

    }
    public class Data:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
