using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Middleware;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfRepository.Repository;
using ConfService.Helper;
using ConfService.Interface;
using ConfService.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Conference
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //configure JwtSettings to inject
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);
            //configure auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(b =>
                {
#if(DEBUG)
                    b.RequireHttpsMetadata = false;
#endif
                    b.SaveToken = true;
                    var bytesKey = Encoding.ASCII.GetBytes(jwtSettingsSection.Get<JwtSettings>().Secret);
                    b.TokenValidationParameters = new TokenValidationParameters()
                    {
                        //todo why true?
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(bytesKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //configure validation
            //some magic code microsoft advised to write to suppress default behavior when
            //ModelState.IsValid == false
            //(when object is invalid default filter automatically sends code 400,
            //and no custom filter checking model state is called)
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });
            
            services.AddAutoMapper();
            services.AddScoped<IConferenceRepository, ConferenceRepository>();
            services.AddScoped<IConferenceService, ConferenceService>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IAdminOfConferenceRepository, AdminOfConferenceRepository>();
            services.AddScoped<ILectureRepository, LectureRepository>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddDbContext<ConfContext>
                (options => options.UseMySQL(Configuration.GetConnectionString("db")));
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
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
