using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonzai.Blazor.Bootstrap.Components
{
    public abstract class CardTitleBase : BootstrapContainerBase
    {
        protected abstract string Element { get; }

        protected override string DefaultClass => "card-title";

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, Element);
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", Classes);
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}
