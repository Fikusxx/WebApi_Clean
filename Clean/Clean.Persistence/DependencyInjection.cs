using Clean.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration config)
    {
        var connection = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<NotesDbContext>(options =>
        {
            options.UseSqlServer(connection);
        });

        services.AddScoped<INotesDbContext, NotesDbContext>();

        return services;
    }

}
