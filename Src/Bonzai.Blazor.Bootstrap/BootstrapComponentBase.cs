using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Bonzai.Blazor.Bootstrap
{
    public abstract class BootstrapComponentBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected string Classes { get; set; }

        protected virtual string DefaultClass { get; }

        protected virtual string CombineClasses()
        {
            if(AdditionalAttributes == null
                || !AdditionalAttributes.ContainsKey("class"))
            {
                return DefaultClass;
            }

            return $"{DefaultClass} {AdditionalAttributes["class"]}";
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Classes = CombineClasses();
        }
    }
}
