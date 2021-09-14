using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Bonzai.Blazor.Bootstrap.Js
{
    public interface IBootstrapJsService
    {
        Task<decimal> GetScrollHeight(ElementReference elementReference);

        Task AddEventListenerAsync(ElementReference elementReference, object objectReference, string eventName, string methodToCall);

        Task<BoundingClientRect> GetBoundingClientRect(ElementReference elementReference);

        Task ReflowAsync(ElementReference elementReference);

        Task RemoveEventListenerAsync(ElementReference elementReference, string eventName, string methodToCall);
    }

    public class BootstrapJsService : IBootstrapJsService
    {
        internal static string JsModuleLocation { get; set; }

        private Task<IJSObjectReference> _bonzaiBootstrapJsReference;

        public BootstrapJsService(IJSRuntime jSRuntime)
        {
            _bonzaiBootstrapJsReference = jSRuntime.InvokeAsync<IJSObjectReference>("import", JsModuleLocation).AsTask();
        }

        public async Task<decimal> GetScrollHeight(ElementReference elementReference)
        {
            var jsReference = await _bonzaiBootstrapJsReference;
            return await jsReference.InvokeAsync<decimal>("getScrollHeight", elementReference);
        }

        public async Task AddEventListenerAsync(ElementReference elementReference, object objectReference, string eventName, string methodToCall)
        {
            var jsReference = await _bonzaiBootstrapJsReference;
            await jsReference.InvokeVoidAsync("addEventListener",
                elementReference,
                eventName,
                DotNetObjectReference.Create(objectReference),
                methodToCall);
        }

        public async Task<BoundingClientRect> GetBoundingClientRect(ElementReference elementReference)
        {
            var jsReference = await _bonzaiBootstrapJsReference;
            return await jsReference.InvokeAsync<BoundingClientRect>("getBoundingClientRect", elementReference);
        }

        public async Task ReflowAsync(ElementReference elementReference)
        {
            var jsReference = await _bonzaiBootstrapJsReference;
            await jsReference.InvokeVoidAsync("reflow", elementReference);
        }

        public async Task RemoveEventListenerAsync(ElementReference elementReference, string eventName, string methodToCall)
        {
            var jsReference = await _bonzaiBootstrapJsReference;
            await jsReference.InvokeVoidAsync("removeEventListener",
                elementReference,
                eventName,
                methodToCall);
        }
    }
}