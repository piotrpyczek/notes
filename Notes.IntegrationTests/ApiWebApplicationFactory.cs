using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Notes.IntegrationTests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var configuration = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.Integration.json")
                     .Build();

                config.AddConfiguration(configuration);
            });
        }

        public HttpClient CreateAutenticatedClient()
        {
            var client = CreateClient();
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJGQjFCODQ5RS0yQ0IzLTRFRDQtQjdGRi1GRTNBMTk0NzdGRTAiLCJ1bmlxdWVfbmFtZSI6IkRldmVsb3BtZW50IiwibmJmIjoxNzA2MzkxMzM4LCJleHAiOjE3MzgwMTM3MzgsImlhdCI6MTcwNjM5MTMzOH0.px32V8qrlXLzqkBRE8UAFmYlUzA74d6wdWmlcd8D8Tk");
            return client;
        }
    }
}
