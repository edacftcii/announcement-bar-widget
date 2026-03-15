using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.AnnouncementBar.Factories;
using Nop.Plugin.Widgets.AnnouncementBar.Services;

namespace Nop.Plugin.Widgets.AnnouncementBar.Infrastructure
{
    public class PluginNopStartup : INopStartup
    {
        #region Methods

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAnnouncementItemService, AnnouncementItemService>();
            services.AddScoped<IAnnouncementModelFactory, AnnouncementModelFactory>();
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 3000;

        #endregion
    }
}