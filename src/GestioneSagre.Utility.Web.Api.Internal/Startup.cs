using GestioneSagre.Shared.RabbitMQ.InputModels;
using GestioneSagre.Tools.MailKit.Options;
using GestioneSagre.Tools.RabbitMQ;
using GestioneSagre.Utility.Business;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Web.Api.Internal.HostedServices;
using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Utility.Web.Api.Internal;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddRabbitMq(settings =>
        {
            settings.ConnectionString = Configuration.GetConnectionString("RabbitMQ");
            settings.ExchangeName = Configuration.GetValue<string>("RabbitMqSettings:ApplicationName");
            settings.QueuePrefetchCount = Configuration.GetValue<ushort>("RabbitMqSettings:QueuePrefetchCount");
        }, queues =>
        {
            queues.Add<EmailMessageInputModel>();
        });

        services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetValue<string>("Database"),
                migration => migration.MigrationsAssembly("GestioneSagre.Utility.Infrastructure"))
        );

        services.AddDbContext<ReadDbContext>(options =>
            options.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetValue<string>("ReadDatabase"),
                migration => migration.MigrationsAssembly("GestioneSagre.Utility.Infrastructure"))
        );

        services.AddSingleton<EmailSenderHostedService>();
        services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<EmailSenderHostedService>());

        services.AddInternalApiService();

        services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseHttpsRedirection();
        app.UseRouting();
    }
}