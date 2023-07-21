using Microsoft.EntityFrameworkCore;
using PlatformService.Data.Repositories;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Database Configuration
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMem"));

//Dependency Injection
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var CommandServiceEndpoint = builder.Configuration.GetValue<string>("CommandService");
System.Console.WriteLine($"=> Command Service Endpoint {CommandServiceEndpoint} <=");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app);

app.Run();
