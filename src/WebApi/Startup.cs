using Lets.Code.Application.Shared;
using Lets.Code.Application.Shared.Middlewares;
using Lets.Code.Application.Shared.Repository;
using Lets.Code.WebApi.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Lets.Code.Application.Shared.Interfaces;
using Lets.Code.Application.Features.Cards;
using Serilog;
using Microsoft.Extensions.Logging;
using System;

namespace Lets.Code.WebApi
{
    public class Startup
    {
        private const string HealthCheckPath = "/health";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {


            services.AddCors();

            services.AddControllers();
            services.AddHealthChecks();

            services.AddDbContext<CardContext>(opt => opt.UseInMemoryDatabase("Cards"));
            services.AddScoped<CardContext>();

            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<ICardApplication, CardApplication>();
            services.AddTransient<IAutenticateRepository, AutenticateRepository>();

            var key = Encoding.ASCII.GetBytes(Settings.GetSecret(Configuration));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lets Code", Version = "v1" });

            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "letscode");
            });

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapApplicationHealthChecks(HealthCheckPath);
            });
        }
    }
}
