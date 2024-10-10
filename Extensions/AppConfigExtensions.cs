using dotenv.net;

namespace peace_api.Extensions
{
    public static class AppConfigExtensions
    {
        public static WebApplication ConfigureCORS(this WebApplication app)
        {
            DotEnv.Load();

            app.UseCors(options =>
            {
                options.AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    // .WithOrigins(Environment.GetEnvironmentVariable("ORIGIN")!)
                    .SetIsOriginAllowed(origin => true);
            });

            return app;
        }
    }
}