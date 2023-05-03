using ECommerceSerilog.Helpers;
using ECommerceSerilog.Repositorys.Info;
using ECommerceSerilog.Repositorys.ReadRepository;
using ECommerceSerilog.Repositorys.WriteRepository;
using ECommerceSerilog.Services.Contract;
using ECommerceSerilog.Services;

namespace ECommerceSerilog
{
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

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();

        }
    }

}
