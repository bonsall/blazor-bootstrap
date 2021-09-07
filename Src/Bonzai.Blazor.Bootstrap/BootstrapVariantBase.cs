using Microsoft.AspNetCore.Components;

namespace Bonzai.Blazor.Bootstrap
{
    public class BootstrapVariantBase : BootstrapComponentBase
    {
        public BootstrapVariantBase() { }

        /// <summary>
        /// Sets the component with a given type. This is used to set defaults.
        /// </summary>
        /// <param name="type"></param>
        public BootstrapVariantBase(string variance)
        {
            Variance = variance;
        }

        [Parameter]
        public string Variance { get; set; }
    }
}
