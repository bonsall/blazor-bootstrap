using Microsoft.AspNetCore.Components;

namespace Bonzai.Blazor.Bootstrap
{
    public class BootstrapVariantContainerBase : BootstrapVariantBase
    {
        public BootstrapVariantContainerBase() { }

        /// <summary>
        /// Sets the component with a given type. This is used to set defaults.
        /// </summary>
        /// <param name="type"></param>
        public BootstrapVariantContainerBase(string variance) : base(variance)
        { }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
