using CashFlow.Application.AppServices;
using CashFlow.Application.Interfaces.Services;
using CashFlow.Domain.Aggregates.CashFlow.Interfaces.Services;
using CashFlow.Domain.Aggregates.CashFlow.Services;
using CashFlow.Infra;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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
//builder.Services.AddScoped<ITransactionRepository, ProductRepository>();
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