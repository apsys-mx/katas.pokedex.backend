using katas.pokedex.repositories;
using katas.pokedex.repositories.nhibernate;

namespace katas.pokedex.webapi
{
    public static class ServiceCollectionExtender
    {



        /// <summary>
        /// Configure CORS
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            string[] allowedCorsOrigins = GetAllowedCorsOrigins(configuration);
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins(allowedCorsOrigins)
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        /// <summary>
        /// Configure the unit of work dependency injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            SessionFactory factory = new SessionFactory(configuration);
            var sessionFactory = factory.BuildNHibernateSessionFactory();
            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static string[] GetAllowedCorsOrigins(IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("AllowedHosts").Value;
            if (string.IsNullOrEmpty(allowedOrigins))
                throw new Exception($"No CORS configuration found in the configuration file");
            return allowedOrigins.Split(",");
        }


    }
}
