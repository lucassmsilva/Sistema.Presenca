using Microsoft.Extensions.DependencyInjection;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;
using Sistema.Infraestrutura.Persistencia.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Sistema.Infraestrutura.Persistencia
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<AppDbContext>(OptionsBuilderExtensions => OptionsBuilderExtensions.UseSqlServer(connectionString));
            services.AddScoped<IUnityOfWork, UnitOfWork>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
        }
    }
}
