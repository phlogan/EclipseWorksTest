using EclipseWorksTest.AppService.DI;
using EclipseWorksTest.Otel;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEclipseServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("Prometheus", client =>
{
    client.BaseAddress = new Uri("http://prometheus:9090/api/v1/");
});
#region :: Otel
builder.Services.AddSingleton<Instrumentation>();
var otel = builder.Services.AddOpenTelemetry();
otel.ConfigureResource(resource => resource
    .AddService(serviceName: builder.Environment.ApplicationName));
otel.WithMetrics(metrics => metrics
    .AddAspNetCoreInstrumentation()
    .AddMeter(Instrumentation.MeterName)
    .AddMeter("Microsoft.AspNetCore.Hosting")
    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
    .AddPrometheusExporter());
#endregion
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Services.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPrometheusScrapingEndpoint();

app.Run();