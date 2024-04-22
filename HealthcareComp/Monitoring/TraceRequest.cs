using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;

namespace Monitoring;

public static class TraceRequest
{
    public static void InjectContext(HttpRequestMessage request)
    {
        var activityContext = Activity.Current?.Context ?? default;
        var propagationContext = new PropagationContext(activityContext, Baggage.Current);
        var propagator = new TraceContextPropagator();
        
        propagator.Inject(propagationContext, request.Headers, (r, key, value) =>
        {
            r.Add(key, value);
        });
    }

}