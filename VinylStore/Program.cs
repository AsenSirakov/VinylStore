using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using VinylStore.HealthChecks;
using VinylStore.MappsterConfig;
using VinylStore.ServiceExtentions;
using VinylStore.Validators;
using VinylStoreBL;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme:
        AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services
    .AddConfigurations(builder.Configuration)
    .RegisterDataLayer()
    .RegisterBusinessLayer();

MappsterConfig.Configure();
builder.Services.AddMapster();


builder.Services
    .AddValidatorsFromAssemblyContaining<AddVinylRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHealthChecks();

builder.Services.AddHealthChecks()
    .AddCheck<SampleHealthCheck>("Sample");

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/Sample");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();