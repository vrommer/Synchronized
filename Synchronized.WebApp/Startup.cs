using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synchronized.WebApp.Services;
using Synchronized.Model;
using Synchronized.Repository.Repositories;
using Synchronized.Core.Repositories;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Interfaces;
using Synchronized.Core;
using UtilsLib.HtmlUtils.HtmlParser;
using Synchronized.Data;
using Microsoft.EntityFrameworkCore;

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
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<SynchronizedDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddUserStore<UsersRepository>()
                .AddRoleStore<RolesRepository>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IDataRepository<Question>, DataRepository<Question>>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<HtmlParser>();
            services.AddTransient<DbContext, SynchronizedDbContext>();

            services.AddMvc();
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
