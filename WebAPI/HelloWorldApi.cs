using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI
{
    public class HelloWorldApi
    {
        [Inject]
        public HelloWorldApiConfiguration Configuration { get; set; }
        [HttpGet]
        public string Get()
        {
            if (Configuration != null)
            {
                var facotry = new EndpointFactory();
                facotry.CreateEndpoint("/hellotest", "WebAPI.HelloWorldApi", new HelloWorldApiConfiguration() { Name = "test" });
                return $"Hello {Configuration.Name}!";
            }
            return "Hello World!";
        }
    }

    public class HelloWorldApiConfiguration
    {
        public string Name { get; set; }
    }
}