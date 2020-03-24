using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterMvcDemo.Filter
{
    public class CustomerResourseFilterAttribute : Attribute, IResourceFilter, IFilterMetadata, IOrderedFilter
    {
        ILogger<CustomerResourseFilterAttribute> _logger;
        public CustomerResourseFilterAttribute(ILogger<CustomerResourseFilterAttribute> logger)
        {
            _logger = logger;
        }
        public int Order => 0;

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation("...........OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation("...........OnResourceExecuting");
        }
    }
}
