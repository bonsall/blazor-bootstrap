using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bonzai.Blazor.Bootstrap
{
    public abstract class BootstrapComponentBase : ComponentBase
    {
        private Queue<Action> _afterRenderActions = new Queue<Action>();
        private Queue<Func<Task>> _afterRenderAsyncActions = new Queue<Func<Task>>();

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected string Classes
        {
            get
            {
                if (AdditionalAttributes == null
                || !AdditionalAttributes.ContainsKey("class"))
                {
                    return DefaultClass;
                }

                return $"{DefaultClass} {AdditionalAttributes["class"]}";
            }
        }

        protected virtual string DefaultClass { get; }

        protected void AfterRender(Action action)
        {
            _afterRenderActions.Enqueue(action);
        }

        protected void AfterRenderOnce(Action action)
        {
            if (_afterRenderActions.Contains(action)) return;

            AfterRender(action);
        }

        protected void AfterRenderAsync(Func<Task> action)
        {
            _afterRenderAsyncActions.Enqueue(action);
        }

        protected void AfterRenderOnceAsync(Func<Task> action)
        {
            if (_afterRenderAsyncActions.Contains(action)) return;

            AfterRenderAsync(action);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            while(_afterRenderActions.Any())
            {
                _afterRenderActions.Dequeue()();
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            while (_afterRenderAsyncActions.Any())
            {
                await _afterRenderAsyncActions.Dequeue()();
            }
        }
    }
}
