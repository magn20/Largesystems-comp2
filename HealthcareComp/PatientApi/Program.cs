using FeatureHubSDK;
using Monitoring;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry().TracingSetup("PatientApi");
builder.Services.AddSingleton(TracerProvider.Default.GetTracer("PatientApi"));
Log.Logger = new LoggerConfiguration()
    .LoggingSetup()
    .CreateLogger();

FeatureLogging.DebugLogger += (sender,s) => Log.Logger.Debug("DEBUG: " + s);
FeatureLogging.TraceLogger += (sender,s) => Log.Logger.Information("DEBUG: " + s);
FeatureLogging.InfoLogger += (sender,s) => Log.Logger.Information("DEBUG: " + s);
FeatureLogging.ErrorLogger += (sender,s) => Log.Logger.Error("ERROR: " + s);

var config = new EdgeFeatureHubConfig("http://featurehub:8085","27ed89c2-84e7-4c52-bb38-c9b218e6c249/BohDZa6N5LxdBfXoRHOuKXwEqOQgpnN9oozF4Ecw");
builder.Services.AddSingleton(config);

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