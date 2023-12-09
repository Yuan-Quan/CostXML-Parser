using Microsoft.AspNetCore.Components;
using WebAPI.Controllers;
using Weikio.ApiFramework.Abstractions;
using Weikio.ApiFramework.Core.Endpoints;

namespace WebAPI
{
    class EndpointFactory
    {
        [Inject]
        public IApiProvider ApiProvider { get; set; }

        [Inject]
        public IEndpointManager EndpointManager { get; set; }

        public EndpointFactory()

        public void CreateEndpoint(string route, String apiDefinition, HelloWorldApiConfiguration configuration)
        {
            var endpoint = new EndpointDefinition(route, apiDefinition, configuration);
            EndpointManager.CreateAndAdd(endpoint);
        }
    }

    class MagicfluAPITestConfiguration
    {
        public String CsvPath { get; set; }
    }
}