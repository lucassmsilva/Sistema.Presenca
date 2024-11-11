using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Sistema.Core.Dominio.Repositories;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.DTO.Presenca;
using Sistema.Core.Aplicacao.Services;

namespace Sistema.Core.Aplicacao
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Registra o Service
            services.AddScoped<IPresencaService<PresencaDTO>, PresencaService<PresencaDTO>>();

            // Registra o Service concreto (já que seu controller está usando a classe concreta)
            services.AddScoped<PresencaService<PresencaDTO>>();

            return services;
        }
    }
}
