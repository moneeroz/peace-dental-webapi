using peace_api.Data;

namespace peace_api.Extensions
{
    public static class EFCoreExtensions
    {
        public static IServiceCollection InjectDBContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>();
            return services;
        }

    }
}