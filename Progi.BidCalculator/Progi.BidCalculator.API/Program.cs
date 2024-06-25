using Progi.BidCalculator.API.Extension;
using Progi.BidCalculator.API.Middleware;
using Progi.BidCalculator.Application;
using Progi.BidCalculator.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
);
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddCalculatorServices();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

if (!app.Environment.IsEnvironment("Test"))
{
    app.InitializeDatabase();
}
app.Run();


public partial class Program { }
