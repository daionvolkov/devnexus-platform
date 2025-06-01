using DevNexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventLog;
using Serilog;
using System.Diagnostics;




var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// Platform-specific logging configuration
if (OperatingSystem.IsWindows())
{
    var eventLogConfig = configuration.GetSection("Logging:EventLog");
    string logName = eventLogConfig["LogName"] ?? "Application";
    string sourceName = eventLogConfig["SourceName"] ?? "YourAppSource";

    if (!EventLog.SourceExists(sourceName))
    {
        EventLog.CreateEventSource(new EventSourceCreationData(sourceName, logName));
    }

    builder.Logging.ClearProviders();
    builder.Logging.AddEventLog(new EventLogSettings
    {
        LogName = logName,
        SourceName = sourceName
    });
}

else
{
    builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration)
    );
}


// Add services to the container.
builder.Services.AddControllers();


// Register the DevNexusDbContext with PostgreSQL support
builder.Services.AddDbContext<DevNexusDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DevNexusDb")));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
