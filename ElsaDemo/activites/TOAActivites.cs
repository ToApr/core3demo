using Elsa;
using Elsa.Attributes;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElsaDemo.activites
{
    [ActivityDefinition(
        Category = "WORKFLOWS",
        DisplayName = "TOA",
        Description = "TOA审批环节",
        Icon = "fas fa-cog",
        Outcomes = new[] { "Success", "NextTOA" }
    )]
    public class TOAActivites: Activity
    {
        [ActivityProperty(Name =  "TOA",Type ="")]
        public string TOA => "TOA";
        public string Message
        {
            get => GetState<string>();
            set => SetState(value);
        }
        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext context)
        {

            var r= false;
            string s= r== true ? "Success" : "NextTOA"; 
            return Outcome(s); 
        }
    }
}
