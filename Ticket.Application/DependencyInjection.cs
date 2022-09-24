using Hangfire;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Ticket.Application.Background;
using Ticket.Application.Interfaces.Background;

namespace Ticket.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHangfire(options => options.UseInMemoryStorage());
            services.AddScoped<IBackgroundJobHandler, BackgroundJobHandler>();

        }
    }
}