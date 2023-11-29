using Newtonsoft.Json;
using System.Net;

namespace ECommerceSerilog.Middlewares;

public class RequestMiddleware
{
    public RequestDelegate requestDelegate;
    private readonly ILogger<RequestMiddleware> _logger;

    public RequestMiddleware(RequestDelegate requestDelegate, ILogger<RequestMiddleware> logger)
    {
        this.requestDelegate = requestDelegate;
        this._logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            int userid = 1;//get user Id

            _logger.LogInformation("request from userId {@userid}", userid);

            _logger.LogInformation("REQUEST HttpMethod: {@HttpMethod}",
                new { context.Request.Method, context.Request.Path });

            await requestDelegate(context);

            _logger.LogInformation("userId = {@userid} successfully handler request", 1);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            var errorMessageObject =
                new { Message = ex.Message, Code = "system_error" };

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        }
    }
}
