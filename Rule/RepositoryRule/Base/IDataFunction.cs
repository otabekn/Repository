using System.Collections.Generic;
namespace RepositoryRule.Base
{
    //change 
    //change name
    public interface IDataFunction
    {
        T CalProcedure<T>(string functionName, params object[] items);
        IEnumerable<T> CallProcedure<T>(string str);
        object CallProcedure(string str);
        

    }
}
