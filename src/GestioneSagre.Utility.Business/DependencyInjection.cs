using GestioneSagre.Tools.MailKit;
using GestioneSagre.Utility.Core;
using GestioneSagre.Utility.Domain.Services;
using GestioneSagre.Utility.Domain.UnitOfWork;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.Utility.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddScoped<DbContext, DataDbContext>();
        services.AddScoped<DbContext, ReadDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssemblyOf<UtilityReadRepository>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.Scan(scan =>
            scan.FromAssemblyOf<SendEmailReadService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }

    public static IServiceCollection AddWorkerService(this IServiceCollection services)
    {
        services.AddTransient<ISendEmailServices, SendEmailServices>();
        services.AddTransient<ISendReadEmailService, SendReadEmailService>();

        services.AddSingleton<IEmailSender, MailKitEmailSender>();
        services.AddSingleton<IEmailClient, MailKitEmailSender>();

        return services;
    }

    public static IServiceCollection AddInternalApiService(this IServiceCollection services)
    {
        services.AddSingleton<ISendEmailServices>(provider => provider.GetRequiredService<ISendEmailServices>());
        services.AddSingleton<ISendReadEmailService>(provider => provider.GetRequiredService<ISendReadEmailService>());

        services.AddTransient<ISendEmailServices, SendEmailServices>();
        services.AddTransient<ISendReadEmailService, SendReadEmailService>();

        services.AddSingleton<IEmailSender, MailKitEmailSender>();
        services.AddSingleton<IEmailClient, MailKitEmailSender>();

        return services;
    }
}