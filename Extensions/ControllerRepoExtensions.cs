using peace_api.Interfaces;
using peace_api.Repository;
using peace_api.Service;

namespace peace_api.Extensions
{
    public static class ControllerRepoExtensions
    {
        public static IServiceCollection AddControllerRepos(this IServiceCollection services)
        {
            // Repositories and Services
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IOverviewRepository, OverviewRepository>();
            services.AddScoped<IRevenueRepository, RevenueRepository>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}