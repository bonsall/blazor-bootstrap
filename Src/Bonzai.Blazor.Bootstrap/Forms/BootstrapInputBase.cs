using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bonzai.Blazor.Bootstrap.Forms
{
    public abstract class BootstrapInputBase<TValue> : BootstrapComponentBase
    {
        public TValue Value { get; set; }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public Expression<Func<TValue>> ValueExpression { get; set; }

        [Parameter]
        public string DisplayName { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public RenderFragment LabelContent { get; set; }

        [Parameter]
        public string FormText { get; set; }

        [Parameter]
        public RenderFragment FormTextContent { get; set; }

        protected string FormTextId { get; private set; }

        protected string Id { get; } = Guid.NewGuid().ToString().Replace("-", "");

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if(AdditionalAttributes == null)
            {
                AdditionalAttributes = new Dictionary<string, object>();
            }

            if(!string.IsNullOrWhiteSpace(Label)
                && !AdditionalAttributes.ContainsKey("id"))
            {
                AdditionalAttributes.Add("id", $"{GetType().Name}-{Id}");
            }

            if (!string.IsNullOrWhiteSpace(FormText))
            {
                FormTextId = $"{GetType().Name}-form-text-{Id}";
                AdditionalAttributes["aria-describedby"] = FormTextId;
            }
        }
    }
}
