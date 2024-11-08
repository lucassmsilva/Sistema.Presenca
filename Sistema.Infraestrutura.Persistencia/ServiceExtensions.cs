using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;
using Sistema.Infraestrutura.Persistencia.Repositories;

namespace Sistema.Infraestrutura.Persistencia
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseType = configuration["DatabaseType"]; // Alternativa direta
            var connectionString = configuration.GetConnectionString(databaseType);

            services.AddDbContext<AppDbContext>(options =>
            {
                if (databaseType == "Postgrees")
                {
                    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Sistema.Infraestrutura.Persistencia"));
                }
                else if (databaseType == "SqlServer")
                {
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Sistema.Infraestrutura.Persistencia"));
                }
                else if (databaseType == "MySql")
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                        b => b.MigrationsAssembly("Sistema.Infraestrutura.Persistencia"));
                }
            });

            services.AddScoped<IUnityOfWork, UnitOfWork>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IPresencaRepository, PresencaRepository>();
            services.AddScoped<IPessoaEnderecoRepository, PessoaEnderecoRepository>();
        }
    }
}