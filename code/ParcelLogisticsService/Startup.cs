using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PLS.SKS.Package;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace PLS.SKS.Package.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddDbContext<DataAccess.Sql.DBContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<BusinessLogic.Interfaces.IBusinessLogicFacade, BusinessLogic.BusinessLogicFacade>();
			services.AddScoped<BusinessLogic.Interfaces.IHopArrivalLogic, BusinessLogic.HopArrivalLogic>();
			services.AddScoped<BusinessLogic.Interfaces.IParcelEntryLogic, BusinessLogic.ParcelEntryLogic>();
			services.AddScoped<BusinessLogic.Interfaces.ITrackingLogic, BusinessLogic.TrackingLogic>();
			services.AddScoped<BusinessLogic.Interfaces.IWarehouseLogic, BusinessLogic.WarehouseLogic>();

			services.AddScoped<DataAccess.Interfaces.IParcelRepository, DataAccess.Sql.SqlParcelRepository>();
			services.AddScoped<DataAccess.Interfaces.IHopArrivalRepository, DataAccess.Sql.SqlHopArrivalRepository>();
			services.AddScoped<DataAccess.Interfaces.IRecipientRepository, DataAccess.Sql.SqlRecipientRepository>();
			services.AddScoped<DataAccess.Interfaces.ITrackingInformationRepository, DataAccess.Sql.SqlTrackingInformationRepository>();
			services.AddScoped<DataAccess.Interfaces.ITruckRepository, DataAccess.Sql.SqlTruckRepository>();
			services.AddScoped<DataAccess.Interfaces.IWarehouseRepository, DataAccess.Sql.SqlWarehouseRepository>();

			services.AddMvc()
				.AddJsonOptions(
			options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
		);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
			//loggerFactory.AddNLog();

			app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
