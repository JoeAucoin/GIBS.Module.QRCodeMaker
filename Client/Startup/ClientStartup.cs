using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using GIBS.Module.QRCodeMaker.Services;

namespace GIBS.Module.QRCodeMaker.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IQRCodeMakerService, QRCodeMakerService>();
        }
    }
}
