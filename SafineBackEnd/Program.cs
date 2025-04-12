using Domain.IRepository;
using FluentValidation;
using MediatR;
using MediatR.Registration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Mongo.Repository;
using MongoDB.Driver;
using SafineBackEnd.Application.Commands.AddLocation;
using SafineBackEnd.Application.Commands.AddPersonToList;
using SafineBackEnd.Application.Commands.EditLocation;
using SafineBackEnd.Application.Commands.RemoveFirstPersonFromList;
using SafineBackEnd.Application.Commands.RemoveLocation;
using SafineBackEnd.Application.Commands.RemovePersonFromList;
using SafineBackEnd.Application.Queries.GetLocationInfo;
using SafineBackEnd.Application.Queries.GetLocationsBySearchArg;
using SafineBackEnd.Application.Queries.GetManagerLocations;
using SafineBackEnd.Application.Queries.GetRemainingTimeInLine;
using SafineBackEnd.Application.Queries.GetTags;
using SafineBackEnd.Services;
using SafineGRPC;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost
                  .CaptureStartupErrors(false)
                  .UseUrls($"https://0.0.0.0:5135")
                  .ConfigureKestrel(kestrel =>
                  {
                      kestrel.Listen(IPAddress.Any, 5135, o => o.Protocols = HttpProtocols.Http2);
                  })
               .UseContentRoot(Directory.GetCurrentDirectory());

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddSingleton<IMongoConfig, MongoConfig>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return configuration.GetSection("DatabaseSettings").Get<MongoConfig>();
});
builder.Services.AddSingleton<IMongoClient, MongoClient>(ctx =>
{
    var config = ctx.GetRequiredService<IMongoConfig>();
    return new MongoClient(config.ConnectionString);
});
builder.Services.AddSingleton(ctx =>
{
    var config = ctx.GetRequiredService<IMongoConfig>();
    var client = ctx.GetRequiredService<IMongoClient>();
    return client.GetDatabase(config.DatabaseName);
});
builder.Services.AddScoped<ITransaction, Transaction>();
builder.Services.AddScoped<IQueryRepository, QueryRepository>();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();

var endpointAssembly = Assembly.GetExecutingAssembly();

builder.Services.AddValidatorsFromAssembly(endpointAssembly);

var serviceConfig = new MediatRServiceConfiguration();

ServiceRegistrar.AddRequiredServices(builder.Services, serviceConfig);

builder.Services.AddTransient<IRequestHandler<AddLocationCommand,StringIdArg>, AddLocationCommandHandler>();
builder.Services.AddTransient<IRequestHandler<AddPersonToListCommand, BoolIdArg>, AddPersonToListCommandHandler>();
builder.Services.AddTransient<IRequestHandler<EditLocationCommand, BoolIdArg>, EditLocationCommandHandler>();
builder.Services.AddTransient<IRequestHandler<RemoveFirstPersonFromListCommand, StringIdArg>, RemoveFirstPersonFromListCommandHandler>();
builder.Services.AddTransient<IRequestHandler<RemoveLocationCommand, BoolIdArg>, RemoveLocationCommandHandler>();
builder.Services.AddTransient<IRequestHandler<RemovePersonFromListCommand, BoolIdArg>, RemovePersonFromListCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetManagerLocationsQuery, LocationMessages>, GetManagerLocationsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetRemainingTimeInLineQuery, IntIdArg>, GetRemainingTimeInLineQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetLocationsBySearchArgQuery, LocationMessages>, GetLocationsBySearchArgQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetLocationInfoQuery, LocationMessage>, GetLocationInfoQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetTagsQuery, GetTagsResponseProto>, GetTagsQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CommandService>();
app.MapGrpcService<QueryService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
