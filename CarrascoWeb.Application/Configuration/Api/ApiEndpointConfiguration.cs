using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrascoWeb.Application.Configuration.Api
{
    public record ApiEndpointConfiguration
    {
        public required string Name { get; set; }

        public required string RequestUri { get; set; }
    }
}
