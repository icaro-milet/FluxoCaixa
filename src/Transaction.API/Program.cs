using System;
using System.Text.Json.Serialization;
using CashFlow.Application.AppServices;
using CashFlow.Application.Interfaces.Services;
using CashFlow.Application.Validators;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;
using CashFlow.Domain.Aggregates.CashFlow.Services;
using CashFlow.Infra;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Repositories;
using CashFlow.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


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

builder.Services.AddDbContext<TransactionContext>(p =>
{
    p.UseNpgsql(builder.Configuration["ConnectionString:Database"],
        w =>
        {
            w.CommandTimeout(40);
            w.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorCodesToAdd: null);
        });
    p.EnableSensitiveDataLogging();
    p.UseQueryTrackingBehavior(Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll);
    p.EnableDetailedErrors();
});

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

app.Run();