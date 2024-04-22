using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;

namespace Monitoring;
public static class Monitoring
{
    public static OpenTelemetryBuilder TracingSetup(this OpenTelemetryBuilder builder, string serviceName)
    {
        return builder.WithTracing(tcb =>
        {
            tcb
                .AddSource(serviceName)
                .AddZipkinExporter(c =>
                {
                    c.Endpoint = new Uri("http://zipkin:9411/api/v2/spans");
                })
                .AddConsoleExporter()
                .SetSampler(new AlwaysOnSampler())
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: serviceName))
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter();
        });
    }
    
    public static LoggerConfiguration LoggingSetup(this LoggerConfiguration loggerConfiguration)  
    {  
        return loggerConfiguration  
            .MinimumLevel.Debug()
            .Enrich.WithSpan() 
            .WriteTo.Console()
            .WriteTo.Seq("http://seq:5341"); 
    }

}
