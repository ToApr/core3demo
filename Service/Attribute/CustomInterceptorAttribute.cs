

using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomInterceptorAttribute : Attribute, IInterceptor
    {
        ILogger<CustomInterceptorAttribute> _logger;
        public CustomInterceptorAttribute(ILogger<CustomInterceptorAttribute> logger)
        {
            _logger = logger;
        }
        public void Intercept(IInvocation invocation)
        {
            try
            {
                _logger.LogError("开始执行拦截器");
                Console.WriteLine("开始执行拦截。。。");
                invocation.Proceed();
                Console.WriteLine("拦截执行完成。。。");

                _logger.LogError("拦截执行完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                _logger.LogError("After service call");
                Console.WriteLine("After service call");
            }
        }

    }
}
