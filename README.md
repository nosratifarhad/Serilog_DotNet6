# Serilog_DotNet6


### First you need to install the following packages :

> Note : Whatever you need, don't install it

``` bash
dotnet add package Serilog.Sinks.Seq
dotnet add package Serilog.Sinks.MSSqlServer
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.AspNetCore
dotnet add package Serilog
```

### Now you must add following code in program.cs 

```csharp
Log.Logger = new LoggerConfiguration()
        //...
        // Set Configuration Between two Line
        //...
        .CreateLogger(); //OR .CreateBootstrapLogger();

```
### your can add configs from 'appsettings.json' OR Hard Code
### if you want user 'appsettings.json' you must add following code before create instance from "LoggerConfiguration"
```csharp
var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();
```

### You can use this command to set all the configurations specified in the appsettings.json file at once, eliminating the need to add the following items separately.
> this code should be added between the two lines above.
```csharp
Log.Logger = new LoggerConfiguration()

    .ReadFrom.Configuration(configuration)

    .CreateLogger();
```
### Note: If you use this command, there is no need to add the items mentioned below separately.

### If you want to add ThreadId to field loggers, you can utilize this method similar to how threads are used.
// If You Dont Need , Remove this Line.
```csharp
Log.Logger = new LoggerConfiguration()
    //...
    .Enrich.With(new ThreadIdEnricher())
    //...
    .CreateLogger();
```
### you shuld create lower class for user to above code .
```csharp
public class ThreadIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "ThreadId", Thread.CurrentThread.ManagedThreadId));
    }
}
```
### To write logs to the console, you can use the following syntax.
// If You Dont Need , Remove this Line.
```csharp
//.WriteTo.Console()
// OR
.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
```
### For Write Logs in Json File , If You Dont Need , Remove this Line.
```csharp
//...
.WriteTo.File(new CompactJsonFormatter(), "log/jsonLog.json", shared: true)
//..
```
![My Remote Image](D:\github\Serilog_DotNet6\imgs\Annotation2.png)
```json
{...},
{
  "Timestamp": "2023-11-29T19:39:08.4114614+03:30",
  "Level": "Information",
  "MessageTemplate": "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms",
  "Properties": {
    "RequestMethod": "POST",
    "RequestPath": "/api/products",
    "StatusCode": 201,
    "Elapsed": 185.8472,
    "SourceContext": "Serilog.AspNetCore.RequestLoggingMiddleware",
    "RequestId": "0HMVGULPKORR8:00000021",
    "ConnectionId": "0HMVGULPKORR8",
    "Application": "ECommerceSerilog"
  },
  "Renderings": {
    "Elapsed": [
      {
        "Format": "0.0000",
        "Rendering": "185.8472"
      }
    ]
  }
}

```
### // For Write Logs in txt File And Set Options From Hear , If You Dont Need , Remove this Line.
```csharp
.WriteTo.File("log/diagnostics.txt")
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
```
### // For Show Logs In Serilog pannel And Set Options From Hear , If You Dont Need , Remove this Line.
```csharp
.WriteTo.Seq("http://localhost:5341", Serilog.Events.LogEventLevel.Warning)
```
### // For Write Logs in txt File And Set Options From "ConfigurationBuilder" , If You Dont Need , Remove this Line.
```csharp
.AuditTo.File("log/diagnostics.txt")
```
### // For Write Logs in Sql Database And Set Options From Hear , If You Dont Need , Remove this Line.

```csharp
.WriteTo.MSSqlServer("Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=LoggingDb;persist security info=True;",
                    new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        SchemaName = "dbo",
                        AutoCreateSqlTable = true
                    })
.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
```
![My Remote Image](D:\github\Serilog_DotNet6\imgs\Annotation5.png)

