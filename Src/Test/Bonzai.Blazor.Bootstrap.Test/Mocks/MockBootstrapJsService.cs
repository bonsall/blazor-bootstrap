using Bonzai.Blazor.Bootstrap.Js;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonzai.Blazor.Bootstrap.Test.Mocks
{
    public class MockBootstrapJsService : Mock<IBootstrapJsService>
    {
        public MockBootstrapJsService() { }

        public MockBootstrapJsService(IServiceCollection services) 
        {
            Register(services);
        }

        public MockBootstrapJsService(MockBehavior mockBehavior) : base(mockBehavior) { }
        public void Register(IServiceCollection services) =>
            services.AddTransient(services => Object);

        public IReturnsResult<IBootstrapJsService> SetupGetBoundingClientRect(BoundingClientRect boundingClientRect) => 
            Setup(x => x.GetBoundingClientRect(It.IsAny<ElementReference>()))
            .ReturnsAsync(boundingClientRect);
    }
}
