using System;
using RepositoryRule.LoggerRepository;
using Serilog.Core;

namespace LoggingRepository
{
    public class SeilogLogger : ILoggerRepository
    {
        private Logger log;

        public SeilogLogger(Logger log)
        {
            this.log = log;
        }

        public void CatchError(string text, long time, object obj, Exception exception, string methodName, int linenumber = 0)
        {
         
        }

        public void Logging(string text, long time, object obj, string methodName, int linenumber = 0)
        {
            log.Information(text+"methodName:{2}, data:{1}, tiem:{0}: lineNumber:{3} ", time, obj, methodName, linenumber);
            
        }

        public void StartFunction(string text, long time, object obj, string methodName, int linenumber = 0)
        {
            Console.WriteLine("sdcsdcd");
        }
    }
}
