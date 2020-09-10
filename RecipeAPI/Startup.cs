using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using DataAccess.EFCore.Repositories;
using Domain.Recipe.Interfaces;
using DataAccess.EFCore.UnitOfWorks;
using DataAccess.Mongo;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace RecipeAPI
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
            services.AddControllers().AddNewtonsoftJson(options =>options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo { Title = "API Docs", Version = "c1" }));

            services.AddOptions<Domain.Recipe.Entities.Recipe>().ValidateDataAnnotations();

            if (false)
            {
                services.AddDbContext<DataAccess.EFCore.ApplicationContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(DataAccess.EFCore.ApplicationContext).Assembly.FullName)
                    )
                );
                #region Repositories
                services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                services.AddTransient<Domain.Recipe.Interfaces.IRecipeRepository, RecipeRepository>();
                #endregion
                services.AddTransient<Domain.Recipe.Interfaces.IUnitOfWork, UnitOfWork>();
            }
            else
            {
                var settings = Configuration.GetSection(nameof(MongoDatabaseSettings)).Get<MongoDatabaseSettings>();
                services.AddSingleton<MongoDatabaseSettings>(settings);
                services.AddSingleton<DataAccess.Mongo.ApplicationContext>();
                #region Repositories
                services.AddTransient(typeof(IGenericRepository<>), typeof(DataAccess.Mongo.Repositories.GenericRepository<>));
                services.AddTransient<Domain.Recipe.Interfaces.IRecipeRepository, DataAccess.Mongo.Repositories.RecipeRepository>();
                #endregion
                services.AddTransient<Domain.Recipe.Interfaces.IUnitOfWork, DataAccess.Mongo.UnitOfWorks.UnitOfWork>();
            }
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty;
            });

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
