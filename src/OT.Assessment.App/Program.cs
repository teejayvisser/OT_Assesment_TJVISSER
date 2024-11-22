using System.Reflection;
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
using OT.Assessment.App.Interfaces;
using OT.Assessment.App.Services;
using OT.Assessment.Consumer.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckl
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
//TODo :move to own class

#region Services
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IWagerService, WagerService>();
#endregion

#region Repository
builder.Services.AddTransient<IWagerRepository, WagerRepository>();
builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
builder.Services.AddTransient<IProviderRepository, ProviderRepository>();
#endregion

builder.Services.Configure<MessageBroker.RabbitMQSetting>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton(typeof(IRabbitMQService<>), typeof(RabbitMQService<>));
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<AppDbContext>(
    options =>
        options.UseSqlServer(
           connectionString,
            x => x.MigrationsAssembly("OT.Assesment.EntityFrameworkCore")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.EnableTryItOutByDefault();
        opts.DocumentTitle = "OT Assessment App";
        opts.DisplayRequestDuration();
    });
}





app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
