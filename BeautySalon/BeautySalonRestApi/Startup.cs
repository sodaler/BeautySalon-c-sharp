using BeautySalonBusinessLogic.BusinessLogics;
using BeautySalonBusinessLogic.OfficePackage;
using BeautySalonBusinessLogic.OfficePackage.Implements;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.StoragesContracts;
using BeautySalonDatabaseImplement.Implements;
using Microsoft.OpenApi.Models;

namespace BeautySalonRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IOrderStorage, OrderStorage>();
            services.AddTransient<IClientStorage, ClientStorage>();
            services.AddTransient<IServiceStorage, ServiceStorage>();
            services.AddTransient<IProcedureStorage, ProcedureStorage>();
            services.AddTransient<ICosmeticStorage, CosmeticStorage>();
            services.AddTransient<IEmployeeStorage, EmployeeStorage>();
            services.AddTransient<IEstimateStorage, EstimateStorage>();
            services.AddTransient<ILaborCostStorage, LaborCostStorage>();

            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IClientLogic, ClientLogic>();
            services.AddTransient<IServiceLogic, ServiceLogic>();
            services.AddTransient<IProcedureLogic, ProcedureLogic>();
            services.AddTransient<ICosmeticLogic, CosmeticLogic>();
            services.AddTransient<IEmployeeLogic, EmployeeLogic>();
            services.AddTransient<IEstimateLogic, EstimateLogic>();
            services.AddTransient<ILaborCostLogic, LaborCostLogic>();
            services.AddTransient<IReportLogic, ReportLogic>();
            services.AddTransient<AbstractSaveToWord, SaveToWord>();
            services.AddTransient<AbstractSaveToExcel, SaveToExcel>();
            services.AddTransient<AbstractSaveToPdf, SaveToPdf>();


            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FlowerShopRestApi",
                    Version = "v1"
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlowerShopRestApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
