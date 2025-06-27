using CashFlow.Worker.Application.Interfaces;
using CashFlow.Worker.Application.Services;
using CashFlow.Worker.Consumers;
using CashFlow.Worker.Domain.Interfaces.Repositories;
using CashFlow.Worker.Infra;
using CashFlow.Worker.Infra.Data;
using CashFlow.Worker.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkerDbContext>(p =>
{
    p.UseNpgsql(builder.Configuration["ConnectionString:Database"], w =>
    {
        w.CommandTimeout(40);
        w.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
    });
    p.EnableSensitiveDataLogging();
    p.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    p.EnableDetailedErrors();
});

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IDailyBalanceRepository, DailyBalanceRepository>();
builder.Services.AddScoped<IDailyConsolidationAppService, DailyConsolidationAppService>();
builder.Services.AddHostedService<ConsolidationWorker>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TransactionCreatedConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("transaction-created-queue", e =>
        {
            e.ConfigureConsumer<TransactionCreatedConsumer>(ctx);
            e.UseMessageRetry(r => r.Interval(2, 100));
            e.ConfigureConsumer<TransactionCreatedConsumer>(ctx);
        });
    });
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);


var app = builder.Build();
await app.RunAsync();