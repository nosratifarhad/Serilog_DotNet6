using ECommerceSerilog;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

#region [ Serilog Configs ]

var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();

Log.Logger = new LoggerConfiguration()

// You can use this command to set all the configurations specified in the appsettings.json file at once,
// eliminating the need to add the following items separately.
// If You Dont Need , Remove this Line.
.ReadFrom.Configuration(configuration)

// If you want to add ThreadId to field loggers, you can utilize this method similar to how threads are used.
// If You Dont Need , Remove this Line.
//.Enrich.With(new ThreadIdEnricher())

// To write logs to the console, you can use the following syntax.
// If You Dont Need , Remove this Line.
//.WriteTo.Console()

// You can give it the following pattern to format the logs in a similar manner.
// If You Dont Need , Remove this Line.
//.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")

// For Write Logs in Json File , If You Dont Need , Remove this Line.
//.WriteTo.File(new CompactJsonFormatter(), "log/jsonLog.json", shared: true)

// For Write Logs in txt File And Set Options From Hear , If You Dont Need , Remove this Line.
//.WriteTo.File("log/diagnostics.txt")
// OR
//.WriteTo.File(
//     Path.Combine("log", "diagnostics.txt"),
//     rollingInterval: RollingInterval.Day,
//     fileSizeLimitBytes: 10 * 1024 * 1024,
//     retainedFileCountLimit: 2,
//     rollOnFileSizeLimit: true,
//     shared: true,
//     flushToDiskInterval: TimeSpan.FromSeconds(1),
//     outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")

// For Show Logs In Serilog pannel And Set Options From Hear , If You Dont Need , Remove this Line.
//.WriteTo.Seq("http://localhost:5341", Serilog.Events.LogEventLevel.Warning)

// For Write Logs in txt File And Set Options From "ConfigurationBuilder" , If You Dont Need , Remove this Line.
//.AuditTo.File("log/diagnostics.txt")

// For Write Logs in Sql Database And Set Options From Hear , If You Dont Need , Remove this Line.
// .WriteTo.MSSqlServer("Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=LoggingDb;persist security info=True;",
//                     new MSSqlServerSinkOptions
//                     {
//                         TableName = "Logs",
//                         SchemaName = "dbo",
//                         AutoCreateSqlTable = true
//                     })
//.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)

//.CreateLogger();
.CreateBootstrapLogger();


#endregion [ Serilog Configs ]

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
