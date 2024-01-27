using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.Commands
{
    public class Bootstrap
    {
        public static void Register(IServiceCollection services)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(currentAssembly));
        }
    }
}
