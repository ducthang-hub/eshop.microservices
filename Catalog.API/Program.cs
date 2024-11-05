using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.PipelineBehaviors;
using BuildingBlocks.Services.Test;
using Carter;
using Catalog.API.Persistent.DatabaseContext;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddDbContextPool<CatalogDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddSingleton<ITest, Test>();

var app = builder.Build();

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.MapCarter();
app.Run();
