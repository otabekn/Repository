using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Newtonsoft.Json;
using RepositoryRule.LoggerRepository;
using Serilog.Core;

namespace LoggingRepository
{

    public class MethodExecuteLoggerInterceptor : AbstractInterceptor
    {
        
        Logger _log;
        public MethodExecuteLoggerInterceptor(Logger log )
        {
        
            _log = log;
        }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await next(context);
                stopwatch.Stop();
                string str = "";
                foreach(var param in context.Parameters)
                {
                    var t=param.GetType();
                    if (t.IsClass)
                    {
                        str += JsonConvert.SerializeObject(param);
                    }
                    else str += param.ToString();
                }
                _log.Information("Executed method {0}.{1}.{2} ({3}) path {4}ms: data:"+str,
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
