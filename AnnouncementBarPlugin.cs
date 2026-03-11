using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Data.Migrations;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.AnnouncementBar
{
    public class AnnouncementBarPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly IWebHelper _webHelper;
        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        public AnnouncementBarPlugin(
            IWebHelper webHelper,
            IPermissionService permissionService,
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _permissionService = permissionService;
            _localizationService = localizationService;
        }

        #endregion

        #region Properties

        public bool HideInWidgetList => false;

        #endregion

        #region Methods

        public override async Task InstallAsync()
        {
            var migrationManager = EngineContext.Current.Resolve<IMigrationManager>();
            migrationManager.ApplyUpMigrations(Assembly.GetExecutingAssembly(), MigrationProcessType.Installation);

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.AnnouncementBar.Menu.Title"] = "Announcement Bar",
                ["Plugins.Widgets.AnnouncementBar.Menu.Configure"] = "Manage Announcements",
                ["Plugins.Widgets.AnnouncementBar.Fields.Text"] = "Text",
                ["Plugins.Widgets.AnnouncementBar.Fields.Color"] = "Color",
                ["Plugins.Widgets.AnnouncementBar.Fields.DisplayOrder"] = "Display Order",
                ["Plugins.Widgets.AnnouncementBar.Fields.IsActive"] = "Is Active"
            });

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
{
    var migrationManager = EngineContext.Current.Resolve<IMigrationManager>();
    migrationManager.ApplyDownMigrations(Assembly.GetExecutingAssembly());

    await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.AnnouncementBar");

    await base.UninstallAsync();
}

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            IList<string> widgetZones = new List<string>
            {
                PublicWidgetZones.HomepageTop
            };

            return Task.FromResult(widgetZones);
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "AnnouncementBar";
        }

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return;

            var menuItem = new SiteMapNode
            {
                SystemName = "Widgets.AnnouncementBar",
                Title = await _localizationService.GetResourceAsync("Plugins.Widgets.AnnouncementBar.Menu.Title"),
                Visible = true,
                IconClass = "far fa-dot-circle",
                Url = $"{_webHelper.GetStoreLocation()}Admin/AnnouncementBar/Configure"
            };

            rootNode.ChildNodes.Add(menuItem);
        }

        #endregion
    }
}