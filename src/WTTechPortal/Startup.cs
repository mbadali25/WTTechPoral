using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WTTechPortal.Data;
using WTTechPortal.Models;
using WTTechPortal.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using WebApiContrib.Core.Formatter.Csv;
using Sakura.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;


namespace WTTechPortal
{
    public class Startup
    {
        /* public Startup(IHostingEnvironment env)
         {
             var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

             if (env.IsDevelopment())
             {
                 // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                 builder.AddUserSecrets();

                 // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                 builder.AddApplicationInsightsSettings(developerMode: true);
             }

             builder.AddEnvironmentVariables();
             Configuration = builder.Build();
         }*/
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var csvFormatterOptions = new CsvFormatterOptions();
            csvFormatterOptions.CsvDelimiter = (',').ToString();
            services.AddApplicationInsightsTelemetry(Configuration);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("IdentityDBConnection")));



            services.AddIdentity<ApplicationUser, WTIdentityRole>()
                  .AddEntityFrameworkStores<ApplicationDbContext>()
                  .AddDefaultTokenProviders();

            // Add application services.


            services.AddDbContext<WttechportalDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("MYSQLConnection"),
            b => b.MigrationsAssembly("ImportExportLocalization")));
            services.AddDbContext<JiraDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("JiraConnection")));

            services.AddMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                //   options.CookieName = ".WTTechPortalSession";
                // options.CookieDomain = "wttechsolutions.com";
            });
            services.AddMvc(options =>
            {
                options.InputFormatters.Add(new CsvInputFormatter(csvFormatterOptions));
                options.OutputFormatters.Add(new CsvOutputFormatter(csvFormatterOptions));
                options.FormatterMappings.SetMediaTypeMappingForFormat("csv", MediaTypeHeaderValue.Parse("text/csv"));
            })
            .AddCsvSerializerFormatters();

            services.AddBootstrapPagerGenerator(options =>
            {
                // Use default pager options.
                options.ConfigureDefault();
            });

            //Enable Caching and Session


            services.AddScoped<ValidateMimeMultipartContentFilter>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();


            services.AddMvc();
        }
        
  
   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();


            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}/{owner?}/{priority?}/{status?}/{closedcheck?}");
            });
        }
    }
}
