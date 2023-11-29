using ECommerceSerilog.Services.Contract;
using ECommerceSerilog.Services;
using ECommerceSerilog.Domain;
using ECommerceSerilog.Infra.Repositories.WriteRepositories.ProductWriteRepositories;
using ECommerceSerilog.Infra.Repositories.ReadRepositories.ProductReadRepositories;

namespace ECommerceSerilog;

public class Startup
{
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        #region [ IOC ]

        #region [ Application ]

        services.AddScoped<IProductService, ProductService>();

        #endregion [Application]

        #region [ Infra - Data ]

        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        #endregion [ Infra - Data EventSourcing ]

        #endregion [ IOC ]
    }

    public void Configure(IApplicationBuilder app)
    {

    }
}
