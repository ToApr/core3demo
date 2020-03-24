using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterMvcDemo.Filter
{
    public class CustomerActionFilter : Attribute, IActionFilter, IFilterMetadata
    {
        ILogger<CustomerActionFilter> logger;
        public CustomerActionFilter(ILogger<CustomerActionFilter> logger)
        {
            this.logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation(context.ActionDescriptor.DisplayName + "----OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation(context.ActionDescriptor.DisplayName + "----OnActionExecuting");

        }
    }

   
}
