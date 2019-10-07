using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synchronized.Repository.Repositories;
using Synchronized.Core.Repositories;
using Synchronized.Domain;
using Synchronized.Data;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using StructureMap;
using Synchronized.WebApp.Services;
using Microsoft.Extensions.Logging;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Interfaces;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.UI.Utilities.Interfaces;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.Core;
using Synchronized.Repository;
using Synchronized.Domain.Factories.Interfaces;
using Synchronized.SharedLib;
using Synchronized.WebApp.Requirements;
using System.Net.Http;

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

            StringBuilder connStrBuilder = new StringBuilder();

            //Load xml
            XDocument xdoc = XDocument.Load("credentials.properties.xml");

            //Run query
            var credentials = xdoc.Descendants("data").First();
            
            connStrBuilder.Append("Uid=")
                .Append(credentials.Descendants("username").First().Value)
                .Append(";Pwd=")
                .Append(credentials.Descendants("password").First().Value)
                .Append(";Host=")
                .Append(credentials.Descendants("host").First().Value)
                .Append(";Database=")
                .Append(credentials.Descendants("database").First().Value);
                
            services.AddScoped<DbContext>(s => new SynchedIdentityDbContext(@connStrBuilder.ToString()));
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection("Logging"))
                    .AddConsole()
                    .AddDebug();
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/Questions/Review", "RequireEditorRole");
                    options.Conventions.AuthorizePage("/Questions/Edit", "RequireEditorRole");
                    options.Conventions.AuthorizePage("/Tags/Create", "RequireEditorRole");
                })
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                })
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireSignedInRole", policy => policy.RequireRole(Constants.SIGNED_USER));
                options.AddPolicy("RequireVoterRole", policy => policy.RequireRole(Constants.VOTER));
                options.AddPolicy("RequireEditorRole", policy => policy.RequireRole(Constants.EDITOR));
                options.AddPolicy("RequireModeratorRole", policy => policy.RequireRole(Constants.MODERATOR));

                options.AddPolicy("HasEnoughPointsToEdit", policy => 
                    policy.Requirements.Add(new NumberOfPointsRequirement(Constants.EDIT_POINST)));
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

            //app.UseHttpsRedirection();

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
                    x.AssemblyContainingType<IPostsConverter>();
                    x.AssemblyContainingType<IEmailSender>();
                    x.AssemblyContainingType<IQuestionsRepository>();
                    x.AssemblyContainingType<IQuestionsService>();
                    x.AssemblyContainingType<ServiceModel.Post>();
                    x.AssemblyContainingType<Post>();
                    x.AssemblyContainingType<ViewServices.Interfaces.IQuestionsService>();
                    x.AssemblyContainingType<IServiceModelFactory>();
                    x.AssemblyContainingType<IViewModelFactory>();
                    x.AssemblyContainingType<IDomainModelFactory>();
                    x.WithDefaultConventions();
                    x.LookForRegistries();
                });
                //_.For<IPostsConverter>().Use<DataConverter>();
                _.For<HttpClient>().Use<HttpClient>();
                _.For<IPostsService<ServiceModel.Comment>>().Use<PostsService<Comment, ServiceModel.Comment>>();
                _.For<IVotedPostService>().Use<VotedPostsService>();
                _.For<IVotedPostRepository>().Use<VotedPostsRepository>();
                _.For<IPostsService<ServiceModel.VotedPost>>().Use<PostsService<VotedPost, ServiceModel.VotedPost>>();
                _.For<IPostsRepository<Comment>>().Use<PostsRepository<Comment>>();
                _.For<IDataRepository<VotedPost>>().Use<PostsRepository<VotedPost>>();
                _.For<IPostsRepository<VotedPost>>().Use<PostsRepository<VotedPost>>();
                //_.For<ITagsConverter>().Use<TagsConverter>();
                _.Populate(services);
            });
            return container.GetNestedContainer().GetInstance<IServiceProvider>();
        }
    }
}
