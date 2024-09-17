using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Globalization;
using TechStack.Domain.Services.Interfaces;
using TechStack.Domain.Shared;
using TechStack.Infrastructure.Data.Dapper;
using TechStack.Infrastructure.Report.NPOI;
using TechStack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TechStack.Extensions;
using TechStack.Infrastructure.Utilities;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechStack.Infrastructure.JWT;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace TechStack.API
{
    public class Startup
    {
        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWTAppSettings:Secret"]))
                };
            });
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        //builder.WithOrigins(AllowedOrgins());
                        builder.AllowAnyOrigin();
                    });
            });
            #region Hangfire
            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("TRMDBConnection")));
            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });
        //    services.AddHangfire(config =>
        //            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        //.UseSimpleAssemblyNameTypeSerializer()
        //.UseDefaultTypeSerializer()
        //.UseMemoryStorage());
            services.AddHangfireServer();
            #endregion
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
                {
                    DateTimeFormat = "dd'-'MM'-'yyyy'T'HH':'mm"
                };

                options.SerializerSettings.Converters.Add(dateConverter);
                options.SerializerSettings.Culture = new CultureInfo("en-GB");
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            });
            #region DB Context
            services.AddDbContext<Infrastructure.Data.Dapper.AppContext>(options =>
                          options.UseSqlServer(
                              Configuration.GetConnectionString("TRMDBConnection")));
            #endregion
            #region Dependency Injection
            services.Configure<GitLabSettingsModel>(Configuration.GetSection("GitLab"));
            services.Configure<JWTAppSettings>(Configuration.GetSection("JWTAppSettings"));

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityProfile());
                mc.AddProfile(new ModelProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IBackgroundJobs, BackgroundJobs>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddTransient<IExcelExport, GenerateExcel>();
            services.AddSingleton<IAppAuthentication, JWTAuthentication>();
            #endregion
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechStack.API", Version = "v1" });
                ////Locate the XML file being generated by ASP.NET...
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                //var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);

                ////... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(XmlCommentsFilePath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                },
            });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, IRecurringJobManager recurringJobManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TRM.GitLab v1"));
            }
            else
            {
               // app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseHangfireDashboard("/jobs");
            app.UseHangfireServer();
            ConfigureRecurringJobs.ConfigureJobs(serviceProvider, recurringJobManager);
            //RecurringJob.AddOrUpdate(
            //    () => Debug.WriteLine("Minutely Job"), Cron.Minutely);
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TRM.GitLab v1"));
        }

        /// <summary>
        /// Get Allowed orgins.
        /// </summary>
        /// <returns>orgins.</returns>
        private string[] AllowedOrgins()
        {
            string[] allowedOrgins = null;

            string origins = this.Configuration["AllowOrigins"];

            if (string.IsNullOrEmpty(origins))
            {
                return allowedOrgins;
            }

            allowedOrgins = origins.Split(',');

            return allowedOrgins;
        }
    }
}
