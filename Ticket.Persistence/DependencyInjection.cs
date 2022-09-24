using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticket.Application.Interfaces.DBContext;
using Ticket.Application.Interfaces.Repositories;
using Ticket.Persistence.DBContext;
using Ticket.Persistence.Repositories;

namespace Ticket.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TicketDBContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(TicketDBContext).Assembly.FullName)));
            services.AddScoped<ITicketDBContext>(provider => provider.GetService<TicketDBContext>());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
