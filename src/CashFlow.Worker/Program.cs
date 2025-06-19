using CashFlow.Worker.Application.Interfaces;
using CashFlow.Worker.Application.Services;
using CashFlow.Worker.Domain.Interfaces.Repositories;
using CashFlow.Worker.Infra;
using CashFlow.Worker.Infra.Data;
using CashFlow.Worker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkerDbContext>(p =>
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

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IDailyBalanceRepository, DailyBalanceRepository>();
builder.Services.AddScoped<IDailyConsolidationAppService, DailyConsolidationAppService>();
builder.Services.AddHostedService<ConsolidationWorker>();

var app = builder.Build();
await app.RunAsync();