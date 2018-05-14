using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synchronized.Repository.Repositories;
using Synchronized.Core.Repositories;
using Synchronized.Model;
using Synchronized.Repository;
using Synchronized.Core;
using Synchronized.Data;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using StructureMap;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Synchronized.WebApp.Services;
using Microsoft.Extensions.Logging;

namespace Synchronized.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddUserStore<UsersRepository>()
                .AddRoleStore<RolesRepository>()
                .AddDefaultTokenProviders();


            //_.For<DbContext>().Use(new SynchronizedDbContext(@"Server = (localdb)\mssqllocaldb; Database = SynchronizedData; Trusted_Connection = true")).Transient();
            services.AddScoped<DbContext>(s => new SynchronizedDbContext(@"Server = (localdb)\mssqllocaldb; Database = SynchronizedData; Trusted_Connection = true"));            

            //services.AddDbContext<SynchronizedDbContext>(b =>
            //{
            //    b.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = SynchronizedData; Trusted_Connection = true");
            //});

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection("Logging"))
                    .AddConsole()
                    .AddDebug();
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                })
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });


            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            //services.AddSingleton<IEmailSender, EmailSender>();

            return ConfigureIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{action}");
            });
        }
        
        private IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = new Container();

            container.Configure(_ =>
            {
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AssemblyContainingType<EmailSender>();
                    x.AssemblyContainingType<QuestionsRepository>();
                    x.AssemblyContainingType<QuestionsService>();
                    x.WithDefaultConventions();
                });

                //_.For<DbContext>().Use<SynchronizedDbContext>();
                //_.For<DbContext>().Use(new SynchronizedDbContext(@"Server = (localdb)\mssqllocaldb; Database = SynchronizedData; Trusted_Connection = true")).AlwaysUnique();
                //_.For(typeof(IdentityDbContext<>)).Use(new SynchronizedDbContext(@"Server = (localdb)\mssqllocaldb; Database = SynchronizedData; Trusted_Connection = true"));
                //_.For<SynchronizedDbContext>().Use(new SynchronizedDbContext(@"Server = (localdb)\mssqllocaldb; Database = SynchronizedData; Trusted_Connection = true"));
                //_.For<DbContext>().Singleton();
                //_.For(typeof(IdentityDbContext<>)).Singleton();
                //_.For<SynchronizedDbContext>().Singleton();

                _.Populate(services);
            });
            return container.GetNestedContainer().GetInstance<IServiceProvider>();
            //return container.GetInstance<IServiceProvider>();
        }
    }
}
