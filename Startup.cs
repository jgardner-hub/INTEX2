using INTEX2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;


namespace INTEX2
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
            services.AddControllersWithViews();

            services.AddDbContext<CrashesDbContext>(options =>
           {
               options.UseMySql(Configuration["ConnectionStrings:CrashesDbConnection"]);

           });

            services.AddSingleton<InferenceSession>(
                new InferenceSession("Models/onnx_intex_model.onnx")
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "countypage",
                    pattern: "Home/CrashSummary/{county}/pg{pageNum}",
                    new {Controller = "Home", action = "CrashSummary"});

                endpoints.MapControllerRoute("Paging", 
                    "Home/CrashSummary/pg{pageNum}",
                    new { Controller = "Home", action = "CrashSummary", pageNum =1 });
                
                endpoints.MapControllerRoute("county",
                    "Home/CrashSummary/{county}",
                    new { Controller = "Home", action = "CrashSummary", pageNum = 1 });

                

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
