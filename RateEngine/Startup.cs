using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using RateEngine.Api.Functions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace RateEngine.Api.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.RegisterRepository();

            builder.RegisterGlobalDependencies();

            builder.RegisterServices();

            builder.RegisterValidators();
        }
    }
}
