using LibraryManagementAPI.Business.Services.Implementations;
using LibraryManagementAPI.Business.Services.Interface;
using LibraryManagementAPI.Business.Utility.Mapping;
using LibraryManagementAPI.Infrastructure.Persistence.Context;
using LibraryManagementAPI.Infrastructure.Persistence.Seeds;
using LibraryManagementCore.Services.Interface;

namespace LibraryManagementAPI.Extensions
{
    public static class DIConfigure
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddAutoMapper(typeof(LibraryMapping));
        }

        public static void UseSeeder(this WebApplication app)
        {
            // Seed the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                DbSeeder.Seed(context);
            }
        }
    }
}
