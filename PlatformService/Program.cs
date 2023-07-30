using Microsoft.EntityFrameworkCore;
using PlatformService.Data.Repositories;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;
using PlatformService.AsyncDataServices;
using PlatformService.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

//Database Configuration
var IsDevelopment = builder.Environment.IsDevelopment();

if (IsDevelopment)
{
    Console.WriteLine("=> Using In-Mem Database. <=");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMem"));
}
else
{
    var connectionString = Configuration.GetConnectionString("PlatformsConn");
    Console.WriteLine("=> Using SQL Server Database. <=");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
}

//Dependency Injection
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var CommandServiceEndpoint = builder.Configuration["CommandService"];
Console.WriteLine($"=> Command Service Endpoint {CommandServiceEndpoint} <=");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();
app.MapGrpcService<GrpcPlatformService>();

app.MapGet("/protos/platforms.proto", async context =>
 {
     await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
 });

PrepDb.PrepPopulation(app, IsDevelopment);


app.Run();
