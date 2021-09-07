using Microsoft.AspNetCore.Components;

namespace Bonzai.Blazor.Bootstrap
{
    public abstract class BootstrapContainerBase : BootstrapComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
