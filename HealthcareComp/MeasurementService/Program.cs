using MeasurementService;
using MeasurementService.Repository;
using Monitoring;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOpenTelemetry().TracingSetup("MeasurementService");
builder.Services.AddSingleton(TracerProvider.Default.GetTracer("MeasurementService"));
Log.Logger = new LoggerConfiguration()
    .LoggingSetup()
    .CreateLogger();

builder.Services.AddScoped<MeasurementService.IMeasurementService, MeasurementService.MeasurementService>();
builder.Services.AddSingleton<IMeasurementRepository, MeasurementRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();