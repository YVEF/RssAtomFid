using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RssAtomFid.Api.DAL;
using RssAtomFid.Api.DAL.Interfaces;
using RssAtomFid.Api.DAL.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using RssAtomFid.Api.Helpers;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace RssAtomFid.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors();            

            services.AddAutoMapper();
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IFeedCollectionRepository, FeedCollectionRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Configuration.GetSection("SecuritySettings:Token").Value)),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.AddMvc(options =>
            {
                options.MaxModelValidationErrors = 7;
                options.AllowValidatingTopLevelNodes = true;
                options.CacheProfiles.Add("EnableCaching", new CacheProfile
                {
                    Duration = (int)TimeSpan.FromMinutes(20).TotalSeconds
                });
                options.CacheProfiles.Add("DisableCaching", new CacheProfile
                {
                    Location = ResponseCacheLocation.None,
                    NoStore = true
                });
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(build =>
                {
                    build.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        IExceptionHandlerFeature error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null) await context.Response.WriteAsync(error.Error.Message);
                    });
                });

                app.UseResponseCompression();
            }

            loggerFactory.AddConsole();


            app.UseAuthentication();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}
