using AngleSharp.Dom;
using Bonzai.Blazor.Bootstrap.Components;
using Bunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bonzai.Blazor.Bootstrap.Test.Assertions
{
    public static class AssertTabPane
    {
        private const string ActiveClass = "active";

        public static void IsActive(IRenderedComponent<TabContainer> cut, string tabPaneSelector)
        {
            var firstPane = cut.Find(tabPaneSelector);
            Assert.Contains(ActiveClass, firstPane.ClassList);
        }

        public static void IsNotActive(IRenderedComponent<TabContainer> cut, string tabPaneSelector)
        {
            IsNotActive(cut.Find(tabPaneSelector));
        }

        public static void IsNotActive(IElement tabPaneElement)
        {
            Assert.DoesNotContain(ActiveClass, tabPaneElement.ClassList);
        }
    }
}
