using ECommerceSerilog;
using ECommerceSerilog.Helpers;
using ECommerceSerilog.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);


#region [ Serilog Configs ]

var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();

Log.Logger = new LoggerConfiguration()
      // For Get ThreadId And Write In Logs , If You Dont Need , Remove this Line.
      .Enrich.With(new ThreadIdEnricher())

    // For Write Logs in Console And Set Options From Hear , If You Dont Need , Remove this Line.
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
     // For Write Logs in Console And Set Options From "ConfigurationBuilder" , If You Dont Need , Remove this Line.
     //.WriteTo.Console()

     // For Write Logs in Json File , If You Dont Need , Remove this Line.
     //.WriteTo.File(new CompactJsonFormatter(), "jsonLog.json", shared: true)

     // For Write Logs in txt File And Set Options From Hear , If You Dont Need , Remove this Line.
     .WriteTo.File("LogFiles/diagnostics.txt")
     .WriteTo.File(
       System.IO.Path.Combine("LogFiles", "diagnostics.txt"),
       rollingInterval: RollingInterval.Day,
       fileSizeLimitBytes: 10 * 1024 * 1024,
       retainedFileCountLimit: 2,
       rollOnFileSizeLimit: true,
       shared: true,
       flushToDiskInterval: TimeSpan.FromSeconds(1),
       outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
          // For Write Logs in txt File And Set Options From "ConfigurationBuilder" , If You Dont Need , Remove this Line.
          //.AuditTo.File("LogFiles/diagnostics.txt")

          // For Show Logs In Serilog pannel And Set Options From Hear , If You Dont Need , Remove this Line.
          //.WriteTo.Seq("http://localhost:5341",
          //    Serilog.Events.LogEventLevel.Warning)

      // For Write Logs in Sql Database And Set Options From Hear , If You Dont Need , Remove this Line.
      .WriteTo.MSSqlServer("Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=LoggingDb;persist security info=True;",
                         new MSSqlServerSinkOptions
                         {
                             TableName = "Logs",
                             SchemaName = "dbo",
                             AutoCreateSqlTable = true
                         })
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)

          
    //.ReadFrom.Configuration(configuration)

          .CreateLogger();


builder.Host.UseSerilog();

#endregion [ Serilog Configs ]

var app = builder.Build();

startup.Configure(app, app.Lifetime);

app.UseMiddleware(typeof(RequestMiddleware));

//app.UseSerilogRequestLogging(options =>
//{
//    // Customize the message template
//    options.MessageTemplate = "Handled {RequestPath}";

//    options.MessageTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}";
//    // Emit debug-level events instead of the defaults
//    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

//    // Attach additional properties to the request completion event
//    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
//    {
//        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
//        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
//    };
//});

app.MapControllers();

app.Run();
