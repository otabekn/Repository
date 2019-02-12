using System;
using RepositoryRule.LoggerRepository;


namespace LoggingRepository
{
    public class SeilogLogger : ILoggerRepository
    {
        
        public void CatchError(string text, long time, object obj, Exception exception, string methodName, int linenumber = 0)
        {
          
        }

        public void Logging(string text, long time, object obj, string methodName, int linenumber = 0)
        {
            Console.WriteLine("sdcsdcs");
        }

        public void StartFunction(string text, long time, object obj, string methodName, int linenumber = 0)
        {
            Console.WriteLine("sdcsdcd");
        }
    }
}
