using Bonzai.Blazor.Bootstrap.CssUtilities;
using Bonzai.Blazor.Bootstrap.Js;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Bonzai.Blazor.Bootstrap.Components
{
    public partial class AccordionItem : BootstrapComponentBase, IAsyncDisposable
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

        private bool _expanded;

        [Parameter]
        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded == value)
                    return;

                _expanded = value;
#pragma warning disable CS4014
                UpdateStylesForExpandedChange(true);
#pragma warning restore CS4014
            }
        }

        [Parameter]
        public EventCallback<bool> ExpandedChanged { get; set; }

        [Parameter]
        public RenderFragment Header { get; set; }

        [Parameter]
        public RenderFragment Body { get; set; }

        [CascadingParameter(Name = "Accordion")]
        public Accordion Accordion { get; set; }

        [Inject]
        private IBootstrapJsService _jsService { get; set; }

        public string BodyId { get; }

        public string HeaderId { get; }

        protected override string DefaultClass { get; } = "accordion-item";

        internal Guid Id { get; }

        private ElementReference BodyElement { get; set; }

        private string _accordionStateClass;

        private string _accordionBodyStyle;

        private bool _isTransitioning;

        private bool _hasRendered;

        private bool _isSynchronousUpdate;

        private string AllBodyClasses
        {
            get
            {
                var classNameBuilder = new ClassNameBuilder("accordion-collapse");
                classNameBuilder.AddClassName(_accordionStateClass);

                if (!string.IsNullOrWhiteSpace(CollapseClasses))
                {
                    classNameBuilder.AddClassName(CollapseClasses);
                }

                return classNameBuilder.GetClassNames();
            }
        }

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

        public async ValueTask DisposeAsync()
        {
            Accordion.RemoveAccordionItem(this);
            await _jsService.RemoveEventListenerAsync(BodyElement,
                "transitionend",
                nameof(SetStableStateStylesAndUpdateState));
        }

        public async Task SetExpandedAsync(bool expanded)
        {
            if (expanded == Expanded)
                return;

            await SetExpandedInternal(expanded);
            StateHasChanged();
        }

        private async Task SetCollapsingStyle()
        {
            _isTransitioning = !_isSynchronousUpdate;

            if (Expanded)
            {
                _accordionStateClass = "collapsing";
            }
            else
            {
                var boundingRect = await _jsService.GetBoundingClientRect(BodyElement);
                _accordionBodyStyle = $"height: {boundingRect.Height}px;";
            }
        }

        [JSInvokable]
        public void SetStableStateStylesAndUpdateState()
        {
            SetStableStateStyles();
            StateHasChanged();
        }

        private void SetStableStateStyles()
        {
            _accordionBodyStyle = null;
            if (Expanded)
            {
                _accordionStateClass = "collapse show";
            }
            else
            {
                _accordionStateClass = "collapse";
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if(Accordion == null)
            {
                throw new ArgumentNullException(nameof(Accordion), $"{typeof(AccordionItem)} was rendered outside of an {typeof(Accordion)}");
            }
            Accordion.RegisterAccordionItem(this);
            SetStableStateStyles();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _hasRendered = true;
            await base.OnAfterRenderAsync(firstRender);

            await _jsService.AddEventListenerAsync(BodyElement, this, "transitionend", nameof(SetStableStateStylesAndUpdateState));

            if (_isSynchronousUpdate)
            {
                _isSynchronousUpdate = false;
                _isTransitioning = true;
                StateHasChanged();
                return;
            }

            if (_isTransitioning)
            {
                if (Expanded)
                {
                    var height = await _jsService.GetScrollHeight(BodyElement);
                    _accordionBodyStyle = $"height: {height}px;";
                }
                else
                {
                    await _jsService.ReflowAsync(BodyElement);
                    _accordionStateClass = "collapsing";
                    _accordionBodyStyle = null;
                }

                _isTransitioning = false;
                StateHasChanged();
            }
        }

        private async Task AccordionButtonClick(MouseEventArgs mouseEvent)
        {
            await SetExpandedInternal(!Expanded);
            await ButtonClicked.InvokeAsync(mouseEvent);
        }

        private async Task SetExpandedInternal(bool expanded)
        {
            _expanded = expanded;
            await ExpandedChanged.InvokeAsync(expanded);

            await UpdateStylesForExpandedChange();
        }

        private async Task RegisterExpandedWithAccordion()
        {
            if (Expanded)
            {
                await Accordion.SetExpandedItem(this);
            }
        }

        private async Task UpdateStylesForExpandedChange(bool isSynchronousUpdate = false)
        {
            // don't run any transitions until after the compnent has rendered.
            if (_hasRendered)
            {
                _isSynchronousUpdate = isSynchronousUpdate;
                await SetCollapsingStyle();
                await RegisterExpandedWithAccordion();
            }
            else
            {
                SetStableStateStyles();
            }
        }
    }
}