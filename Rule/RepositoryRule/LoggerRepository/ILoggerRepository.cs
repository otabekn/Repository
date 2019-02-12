using System;

namespace RepositoryRule.LoggerRepository
{
    public interface ILoggerRepository
    {
        void StartFunction(string text, long time, object obj, string methodName, int linenumber = 0);
        void Logging(string text, long time, object obj, string methodName, int linenumber=0);
         void CatchError(string text, long time, object obj, Exception exception, string methodName, int linenumber = 0);
        
    }
}
