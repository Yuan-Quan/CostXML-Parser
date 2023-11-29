using WebAPI.Controllers;
using Weikio.ApiFramework;
using Weikio.ApiFramework.Abstractions;
using Weikio.ApiFramework.Core.StartupTasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
            builder.Services.AddApiFramework()
                .AddApi<MagicfluAPITestController>()
                .AddEndpoint<MagicfluAPITestController>("/magicfluapitest");
            builder.Services.AddOpenApiDocument();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }

}