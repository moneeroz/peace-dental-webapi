using peace_api.Interfaces;
using peace_api.Models;
using peace_api.Service;
using peace_api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

builder.Services.AddSwaggerExplorer()
                .InjectDBContext()
                .AddControllerRepos()
                .AddIdentityHandlersAndStors()
                .ConfigureIdentityOptions()
                .AddIdentityAuth();

var app = builder.Build();

app.ConfigureSwaggerExplorer();

app.UseHttpsRedirection();

app.ConfigureCORS();

app.AddIdentityAuthMiddlewares();

app.MapControllers();

app
    // .MapGroup("/api")
    .MapIdentityApi<AppUser>();

app.Run();
