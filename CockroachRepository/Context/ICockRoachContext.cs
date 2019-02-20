using SqlKata.Execution;


namespace CockroachRepository.Context
{
    public  interface ICockRoachContext
    {
        QueryFactory Db { get;  }
    }
}
