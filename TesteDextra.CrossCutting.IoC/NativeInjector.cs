using TesteDextra.Infra.UoW;
using TesteDextra.Infra.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using TesteDextra.Application.Services;
using TesteDextra.Domain.Interfaces.Services;
using TesteDextra.Domain.Interfaces.Repository;
using TesteDextra.Domain.Services;
using TesteDextra.Infra.Repository;

namespace TesteDextra.CrossCutting.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Application

            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            services.AddScoped<Application.Interfaces.IPedidosApplication, PedidoApplication>();
            services.AddScoped<Application.Interfaces.ILanchesApplication, LanchesApplication>();

            services.AddScoped<Application.Interfaces.IComplementosApplication, ComplementosApplication>();

            #endregion

            #region Domain
            services.AddScoped<IPedidosDomain, PedidosDomain>();
            services.AddScoped<ILancheDomain, LancheDomain>();
            services.AddScoped<IComplementosDomain, ComplementoDomain>();

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<ILancheRepository, LancheRepository>();
            services.AddScoped<IComplementosRepository, IngredienteRepository>();
            services.AddScoped<IIngredienteRepository, IngredienteRepository>();
            services.AddScoped<IPedidoIngredienteRepository, PedidoIngredienteRepository>();
            services.AddScoped<IParametroRepository, ParametroRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region  Infra

            services.AddScoped<TesteDextraContext>();

            //services.AddScoped<ICustomerRepository, CustomerRepository>();

            #endregion

            #region Location

            // services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            // services.AddScoped<IEventStore, SqlEventStore>();
            // services.AddScoped<EventStoreSQLContext>();

            #endregion

        }
    }
}
