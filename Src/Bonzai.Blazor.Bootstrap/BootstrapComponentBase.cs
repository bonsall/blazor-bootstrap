﻿using Microsoft.AspNetCore.Components;
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

        protected void AfterRender(Func<Task> action)
        {
            _afterRenderAsyncActions.Enqueue(action);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            while(_afterRenderActions.Any())
            {
                _afterRenderActions.Dequeue()();
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            while (_afterRenderAsyncActions.Any())
            {
                await _afterRenderAsyncActions.Dequeue()();
            }
        }
    }
}
