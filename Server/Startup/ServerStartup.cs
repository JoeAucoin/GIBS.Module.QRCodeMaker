using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using GIBS.Module.QRCodeMaker.Repository;
using GIBS.Module.QRCodeMaker.Services;
using QRCoder;

namespace GIBS.Module.QRCodeMaker.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IQRCodeMakerService, ServerQRCodeMakerService>();
            services.AddDbContextFactory<QRCodeMakerContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
