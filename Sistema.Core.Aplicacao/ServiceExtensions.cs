using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Sistema.Core.Aplicacao
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
