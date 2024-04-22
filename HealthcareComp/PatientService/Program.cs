using Monitoring;
using OpenTelemetry.Trace;
using PatientService;
using PatientService.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry().TracingSetup("PatientService");
builder.Services.AddSingleton(TracerProvider.Default.GetTracer("PatientService"));

builder.Services.AddScoped<IPatientService, PatientService.PatientService>();

builder.Services.AddSingleton<IPatientRepository, PatientRepository>();

Log.Logger = new LoggerConfiguration()
    .LoggingSetup()
    .CreateLogger();

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