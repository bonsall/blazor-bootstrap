using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonzai.Blazor.Bootstrap.Test
{
    public class TestBase<TComponent> : TestContext
        where TComponent : ComponentBase
    {
        protected IRenderedComponent<TComponent> RenderComponent(RenderFragment renderFragment) =>
            Render<TComponent>(renderFragment);
    }
}
