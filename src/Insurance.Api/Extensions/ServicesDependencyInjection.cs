using Insurance.Api.Services;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Services;
using Insurance.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Api.Extensions
{
    /// <summary>
    /// ServicesDependencyInjection.
    /// </summary>
    public static class ServicesDependencyInjection
    {
        /// <summary>
        /// ServiceDependencies.
        /// </summary>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection ServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProductAPIService, ProductAPIService>();
            services.AddScoped<IProductTypeWithSurchargeService, ProductTypeWithSurchargeService>();
            services.AddScoped<IProductTypeWithSurchargeRepository, ProductTypeWithSurchargeRepository>();
            return services;
        }
    }
}
