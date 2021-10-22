using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Bonzai.Blazor.Bootstrap.Components
{
    public abstract class CardSubtitleBase : BootstrapContainerBase
    {
        protected abstract string Element { get; }

        protected override string DefaultClass => "card-subtitle";

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