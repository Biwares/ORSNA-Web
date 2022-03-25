using BD.Models;
using BL;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Orsna.Helpers;
using System;
using System.IO;

namespace Orsna
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
            /*var connection = @"data source=10.230.20.21;initial catalog=OrsnaDatabase;user id=rsaadmin;password=rsa123;MultipleActiveResultSets=True;";
            services.AddDbContext<OrsnaDatabaseContext>(options => options.UseSqlServer(connection));*/
            //END EF CORE

            services.AddDbContext<OrsnaDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OrsnaDatabaseEntities")));

            //uso de cookies en sessiones
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //uso de sesiones y HttpContext en Asp.Net core y MVC Core
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });
            //EN SESSIONS


            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            /*Add elmah*/
            //DB
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("ElmahConfig");
                options.Path = "/ver/los/errores";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            //tomar en cuenta el orden de carga, ya que produce excepciones
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                //habilitacion de cache de memoria para almacenar las sessiones de usuario
                app.UseSession();

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            /*Add elmah*/
            app.UseElmah();


            //UPLOAD FILES ////////////////////////////////////////////////////////////////////
            try
            {
                app.UseStaticFiles();

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, Configuration.GetValue<string>("MyConfig:UploadFolder"))),
                    //FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")),
                    RequestPath = new PathString("/Uploads")
                });
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
            }
            ///////////////////////////////////////////////////////////////////////////
            ///
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = new TimeSpan(0, 0, 120);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<OrsnaDatabaseContext>().Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                ElmahCore.ElmahExtensions.RiseError(new Exception("Failed to migrate or seed database" + ex));
            }
        }
    }
}
