# Serilog In .Net 6

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
        .CreateLogger();
```
### your can add configs from 'appsettings.json' OR Hard Code
### if you want user 'appsettings.json' you must add following code before create instance from "LoggerConfiguration"
```csharp
var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();
```
### now you have all configs in variable name 'configuration' and you can add to following code
> this code should be added between the two lines above.
```csharp
.ReadFrom.Configuration(configuration)
```
### for get ThreadId and write in logs , you use this code.
> this code should be added between the two lines above code.
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
### for write logs on console , your should set options from hard coding
```csharp
.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
// if you want add options from configuration , so uncomment lower code and comment top code .
//.WriteTo.Console()
```
![My Remote Image](https://github.com/nosratifarhad/Serilog_DotNet6/blob/main/imgs/Annotation4.jpg)
### for write logs on json file 
```csharp
//...
.WriteTo.File(new CompactJsonFormatter(), "jsonLog.json", shared: true)
//..
```
![My Remote Image](https://github.com/nosratifarhad/Serilog_DotNet6/blob/main/imgs/Annotation5.jpg)

### for write logs on txt File , your should set options from hard coding
```csharp
.WriteTo.File(
       "LogFiles/diagnostics.txt",
       outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
// if you want add options from configuration ,so uncomment lower code and comment top code .
//.WriteTo.File("LogFiles/diagnostics.txt")
```
![My Remote Image](https://github.com/nosratifarhad/Serilog_DotNet6/blob/main/imgs/Annotation3.jpg)
![My Remote Image](https://github.com/nosratifarhad/Serilog_DotNet6/blob/main/imgs/Annotation2.jpg)
### for write logs on sql server , your should set options from hard coding
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
![My Remote Image](https://github.com/nosratifarhad/Serilog_DotNet6/blob/main/imgs/Annotation.jpg)
### more ...
```csharp
.WriteTo.Seq("http://localhost:5341",
    Serilog.Events.LogEventLevel.Warning)

.AuditTo.File("LogFiles/diagnostics.txt")
```
# coming soon set to kibana in docker ;)


