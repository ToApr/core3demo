using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterMvcDemo.Filter
{
    public class CustomerResultFilterAttribute : IResultFilter
    {
        ILogger<CustomerResultFilterAttribute> _logger;
        public CustomerResultFilterAttribute(ILogger<CustomerResultFilterAttribute> logger)
        {
            _logger = logger;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation(context.ActionDescriptor.DisplayName + "---------OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation(context.ActionDescriptor.DisplayName + "---------OnResultExecuting");
        }
    }
}
