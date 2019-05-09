using AspNetCore.Security.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleApiWithAuth.Api;
using Swashbuckle.AspNetCore.Swagger;

namespace SimpleApiWithAuth
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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "OpenSkating API", Version = "1"}); });


            var securitySettings = new SecuritySettings();
            this.Configuration.Bind("SecuritySettings", securitySettings);

            services.AddSecurity<Authenticator, UserModel>(this.Configuration, builder =>
                    builder.AddClaim(IdType.Name, userModel => userModel.Id)
                        .AddClaim(IdType.Role, userModel => userModel.Role)
                , true);

            services
                .AddCors()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddSecurity<UserModel>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // enable middleware to serve the swagger interface and specify the url of JSON endpoint
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1"); });

            app.UseSecurity(true);
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}