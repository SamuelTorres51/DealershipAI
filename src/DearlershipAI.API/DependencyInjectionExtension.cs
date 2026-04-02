using DearlershipAI.API.Models.Repositories;
using DearlershipAI.API.Models.Repositories.Cars;
using DearlershipAI.API.Repositories.DataAccess;
using DearlershipAI.API.Repositories.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace DearlershipAI.API;

public static class DependencyInjectionExtension {
    public static void AddInjection(this IServiceCollection services, IConfiguration configuration) {
        AddRepositories(services);
        AddDbContext(services, configuration);
        AddAutoMapper(services);
    }

    private static void AddRepositories(IServiceCollection services) {
        services.AddScoped<ICarWriteOnlyRepository, CarRepository>();
        services.AddScoped<IUnityOfWork, UnityOfWork>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("Connection");
        if (connectionString is not null) {
            services.AddDbContext<DearlershipDbContext>(options => options.UseMySQL(connectionString));
        }
    }

    private static void AddAutoMapper(IServiceCollection services) {
        services.AddAutoMapper(cfg => { }, typeof(AutoMapping));
    }


}
