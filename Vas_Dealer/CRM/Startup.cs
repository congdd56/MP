using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VAS.Dealer.Authentication;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.VAS.VINAService;
using VAS.Dealer.Provider;
using VAS.Dealer.Services;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer
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
            #region Token and Cookies

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("UserTokenSetting:Secret").Value);
            var validationParams = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = true,
                ValidateIssuer = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            services.AddDataProtection(options =>
                options.ApplicationDiscriminator = "DPL.CRM@2020")
                .SetApplicationName("MP.DPL.CRM.Voi-ip");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme =
                CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                CookieAuthenticationDefaults.AuthenticationScheme;

            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = options.LoginPath;
                options.ReturnUrlParameter = "returnUrl";
            });
            #endregion

            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().AddMvcOptions(options =>
            {
                options.MaxModelValidationErrors = 999999;
            }).AddRazorRuntimeCompilation();
            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddApiVersioning();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CRM DPL API",
                    Description = "Danh sách API",
                    TermsOfService = new Uri("https://wiki.asterisk.org/wiki/display/AST/Asterisk+16+AMI+Actions"),
                    Contact = new OpenApiContact
                    {
                        Name = "Binhnc",
                        Email = string.Empty,
                        Url = new Uri("https://www.youtube.com/watch?v=pbRUVr3P43I"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MP team",
                        Url = new Uri("https://mpfamily.vn/"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<MP_Context>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DBConnection"));
            });

            services.AddDbContext<MM_Context>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            services.Configure<VINAServiceModel>(Configuration.GetSection("VINA_service"));
            services.Configure<FTPConfigModel>(Configuration.GetSection("FTPConfigs"));
            services.Configure<UserTokenSettingModel>(Configuration.GetSection("UserTokenSetting"));


            #region Admin
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ITokenServices, TokenServices>();
            services.AddTransient<IRoleServices, RoleServices>();
            services.AddTransient<IPermissionServices, PermissionServices>();
            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<IMemoryServices, MemoryServices>();
            services.AddTransient<IEmailServices, EmailServices>();

            services.AddScoped<EnsureUserLoggedIn>();

            #endregion

            #region VAS
            services.AddTransient<IVASServices, VASServices>();
            services.AddTransient<IAPIServices, APIServices>();
            services.AddTransient<IFTPServices, FTPServices>();
            services.AddTransient<ICDRServices, CDRServices>();
            services.AddTransient<ILottoServices, LottoServices>();

            #endregion

            //services.AddHostedService<VINAHostedToken>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //first handle any websocket requests
            app.UseWebSockets();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V9 API V1");
                });
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();
                        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        {
                            await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
                        }

                        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<CCEvents>("/cCEvents");
            });
        }
    }
}
