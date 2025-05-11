public class TraceIdLoggingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var traceId = context.TraceIdentifier;
        using (Serilog.Context.LogContext.PushProperty("TraceId", traceId))
        {
            await next(context);
        }
    }
}
