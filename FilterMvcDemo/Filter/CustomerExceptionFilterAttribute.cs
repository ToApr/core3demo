using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterMvcDemo.Filter
{
    public class CustomerExceptionFilterAttribute : Attribute, IExceptionFilter
    {

        public ILogger logger { get; set; }
        public CustomerExceptionFilterAttribute(ILogger<CustomerActionFilter> logger)
        {
            this.logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            logger.LogWarning(System.Text.Json.JsonSerializer.Serialize(context));
            context.ExceptionHandled = true;
        }
    }
}
