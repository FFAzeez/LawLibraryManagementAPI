using LibraryManagementAPI.Infrastructure.Persistence.Context;
using LibraryManagementAPI.Infrastructure.Persistence.Repositories.Implementations;
using LibraryManagementAPI.Infrastructure.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementAPI.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}