using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Application;
public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var callingAssemnly = Assembly.GetCallingAssembly();
        var assembly = Assembly.GetExecutingAssembly();
        services
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
