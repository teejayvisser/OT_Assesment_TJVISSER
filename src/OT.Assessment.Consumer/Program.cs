using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OT.Assesment.EntityFrameworkCore;
using OT.Assesment.EntityFrameworkCore.Mapping;
using OT.Assesment.EntityFrameworkCore.Repositories.GameRepository;
using OT.Assesment.EntityFrameworkCore.Repositories.PlayerRepository;
using OT.Assesment.EntityFrameworkCore.Repositories.ProviderRepository;
using OT.Assesment.EntityFrameworkCore.Repositories.WagerRepository;
using OT.Assesment.Shared.RabbitMq;
using OT.Assessment.Consumer;
using OT.Assessment.Consumer.Models;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    })
    .ConfigureServices((context, services) =>
    {
        // RabbitMQ Configuration
        services.Configure<MessageBroker.RabbitMQSetting>(context.Configuration.GetSection("RabbitMQ"));
// Register the consumer service as a hosted service only
        services.AddHostedService<WagerConsumptionService>();
        services.AddTransient<IWagerRepository, WagerRepository>();
        services.AddTransient<IGameRepository, GameRepository>();
        services.AddTransient<IPlayerRepository, PlayerRepository>();
        services.AddTransient<IProviderRepository, ProviderRepository>();

        services.AddSingleton(typeof(IRabbitMQService<>), typeof(RabbitMQService<>));

        var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        var connectionString = context.Configuration.GetConnectionString("DatabaseConnection");
        services.AddDbContext<AppDbContext>(
            options =>
                options.UseSqlServer(
                    connectionString,
                    x => x.MigrationsAssembly("OT.Assesment.EntityFrameworkCore")));
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application started {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

await host.RunAsync();

logger.LogInformation("Application ended {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);