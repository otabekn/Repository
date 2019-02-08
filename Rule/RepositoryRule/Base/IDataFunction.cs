using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RepositoryRule.Base
{
    //change 
    //change name
    public interface IDataFunction
    {
        T CalProcedure<T>(string functionName, params object[] items);
        IEnumerable<T> CallProcedure<T>(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        object CallProcedure(string str);
        

    }
}
