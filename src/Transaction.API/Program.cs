using System.Text.Json.Serialization;
using CashFlow.Application.AppServices;
using CashFlow.Application.Interfaces.Services;
using CashFlow.Application.Validators;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;
using CashFlow.Domain.Aggregates.CashFlow.Services;
using FluentValidation.AspNetCore;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Repositories;
using CashFlow.Infra.Repositories;
using CashFlow.Worker.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(CashFlow.Application.Mappings.TransactionMappingProfile));

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining(typeof(CreateTransactionsRequestValidator));
    })
    .AddJsonOptions(options =>
    {
        // Serializa enums como string no JSON
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddApiVersioning(
    options =>
    {
        // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
        options.ReportApiVersions = true;
    });
builder.Services.AddVersionedApiExplorer(
    options =>
    {
        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
        // note: the specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
        // can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//IoC
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionAppService, TransactionAppService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();



builder.Services.AddMassTransit();
builder.Services.AddMassTransitHostedService();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});


builder.Services.AddDbContext<CashFlow.Infra.TransactionContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"), npgsqlOptions =>
    {
        npgsqlOptions.CommandTimeout(40);
        npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
    });
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    options.EnableDetailedErrors();
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CashFlow.Infra.TransactionContext>();
    context.Database.Migrate(); 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();