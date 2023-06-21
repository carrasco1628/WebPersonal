using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrascoWeb.Application.Configuration.Api
{
    public record ApiConfiguration
    {
        public required List<ApiConfigurationEntry> ApiConfigurations { get; set; }
    }
}
