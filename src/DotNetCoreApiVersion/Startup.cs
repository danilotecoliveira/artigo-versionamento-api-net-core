using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetCoreApiVersion
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
            services.AddControllers();

            services.AddApiVersioning(x =>
            {  
                // Especifica a versão padrão da API
                x.DefaultApiVersion = new ApiVersion(1, 0);
                // Assume que DefaultApiVersion será a padrão se o cliente não especificar nenhuma
                x.AssumeDefaultVersionWhenUnspecified = true; 
                // Exibe no header a versão da API
                x.ReportApiVersions = true;
                // Especifica que a versão será utilizada no header
                x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
