using Microsoft.AspNetCore.Components;
using System;

namespace Bonzai.Blazor.Bootstrap.Documentation.Client.Pages.Components
{
    public class DocsBase : ComponentBase
    {
        private int _exampleNumber;

        protected int ExampleNumber
        {
            get
            {
                return ++_exampleNumber;
            }
        }
    }
}