
using FluentValidation;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RateEngine.Api.Functions.Infrastructure.Behaviours;
using RateEngine.Application.Features.Rate.Get;
using RateEngine.Core.Interfaces.Repositories;
using RateEngine.Core.Interfaces.Service;
using RateEngine.Infrastructure.Repositories;
using RateEngine.Infrastructure.Services;
using RestSharp;
namespace RateEngine.Api.Functions
{
    public static class StartupExtensions
    {
        public static void RegisterRepository(this IFunctionsHostBuilder builder)
        {
            
            builder.Services.AddSingleton<ISpecialRateRepository, SpecialRateRepository>();
            builder.Services.AddSingleton<IStandardRateRepository, StandardRateRepository>();
        }

        public static void RegisterServices(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<ISpecialRateService, SpecialRateService>();
            builder.Services.AddTransient<IStandardRateService, StandardRateService>();
        }

        public static void RegisterValidators(this IFunctionsHostBuilder builder)
        { 
            builder.Services.AddSingleton<IValidator<Query>, Validator>();
        }

        public static void RegisterGlobalDependencies(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediatR(typeof(Handler));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddTransient<IRestClient, RestClient>();
        }
    }
}
