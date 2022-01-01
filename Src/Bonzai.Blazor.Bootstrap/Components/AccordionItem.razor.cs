using Bonzai.Blazor.Bootstrap.CssUtilities;
using Bonzai.Blazor.Bootstrap.Js;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Bonzai.Blazor.Bootstrap.Components
{
    public partial class AccordionItem : BootstrapComponentBase, IDisposable
    {
        public AccordionItem()
        {
            Id = Guid.NewGuid();
            BodyId = $"accordion-collapse-{Id}";
            HeaderId = $"accordion-header-{Id}";
        }

        [Parameter]
        public EventCallback<MouseEventArgs> ButtonClicked { get; set; }

        [Parameter]
        public string HeaderClasses { get; set; }

        [Parameter]
        public string HeaderButtonClasses { get; set; }

        [Parameter]
        public string CollapseClasses { get; set; }

        [Parameter]
        public bool Expanded { get; set; }

        [Parameter]
        public EventCallback<bool> ExpandedChanged { get; set; }

        [Parameter]
        public RenderFragment Header { get; set; }

        [Parameter]
        public RenderFragment Body { get; set; }

        [CascadingParameter(Name = "Accordion")]
        public Accordion Accordion { get; set; }

        public string BodyId { get; }

        public string HeaderId { get; }

        protected override string DefaultClass { get; } = "accordion-item";

        internal Guid Id { get; }

        private string AllButtonClasses
        {
            get
            {
                var classNameBuilder = new ClassNameBuilder("accordion-button");

                if (!Expanded)
                {
                    classNameBuilder.AddClassName("collapsed");
                }

                if (!string.IsNullOrWhiteSpace(HeaderButtonClasses))
                {
                    classNameBuilder.AddClassName(HeaderButtonClasses);
                }

                return classNameBuilder.GetClassNames();
            }
        }

        string ExpandableClasses
        {
            get
            {
                var classNameBuilder = new ClassNameBuilder("accordion-collapse");

                if(!string.IsNullOrWhiteSpace(CollapseClasses))
                {
                    classNameBuilder.AddClassName(CollapseClasses);
                }

                return classNameBuilder.GetClassNames();
            }
        }

        public async Task SetExpandedAsync(bool expanded)
        {
            if (expanded == Expanded)
                return;

            await SetExpandedInternal(expanded);
            StateHasChanged();
        }

        public void Dispose()
        {
            Accordion.RemoveAccordionItem(this);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if(Accordion == null)
            {
                throw new ArgumentNullException(nameof(Accordion), $"{typeof(AccordionItem)} was rendered outside of an {typeof(Accordion)}");
            }
            Accordion.RegisterAccordionItem(this);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            bool expandedChanged = parameters.TryGetValue<bool>(nameof(Expanded), out var expanded)
                && expanded != Expanded;
            
            await base.SetParametersAsync(parameters);

            if(expandedChanged)
            {
                Expanded = expanded;
                await RegisterExpandedWithAccordion();
            }
        }


        private async Task AccordionButtonClick(MouseEventArgs mouseEvent)
        {
            await SetExpandedInternal(!Expanded);
            await ButtonClicked.InvokeAsync(mouseEvent);
        }

        private async Task SetExpandedInternal(bool expanded)
        {
            Expanded = expanded;
            await ExpandedChanged.InvokeAsync(expanded);
            await RegisterExpandedWithAccordion();
        }

        private async Task RegisterExpandedWithAccordion()
        {
            if (Expanded)
            {
                await Accordion.SetExpandedItem(this);
            }
        }

    }
}