using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using RepositoryRule.LoggerRepository;

namespace LoggingRepository
{

    public class MethodExecuteLoggerInterceptor : AbstractInterceptor
    {
        ILoggerRepository _logger;
        public MethodExecuteLoggerInterceptor(ILoggerRepository logger)
        {
            _logger = logger;
        }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {

            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.Logging("joha", 123, null, "sdcsd", 132);
                await next(context);
                stopwatch.Stop();

                Console.WriteLine("Executed method {0}.{1}.{2} ({3}) in {4}ms",
                context.ImplementationMethod.DeclaringType.Namespace,
                context.ImplementationMethod.DeclaringType.Name,
                context.ImplementationMethod.Name,
                context.ImplementationMethod.DeclaringType.Assembly.GetName().Name,
                stopwatch.ElapsedMilliseconds
            );
            }
            catch(Exception ext)
            {

            }
            
            
            
        }
    }

}
