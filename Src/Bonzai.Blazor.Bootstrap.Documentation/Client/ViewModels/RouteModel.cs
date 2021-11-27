using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bonzai.Blazor.Bootstrap.Documentation.Client.ViewModels
{
    public class RouteModel
    {
        public string Route { get; set; }

        public string DisplayName { get; set; }

        public List<RouteModel> ChildRoutes { get; set; }
    }
}
