using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Customers;
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

        #region Install / Uninstall

        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.AnnouncementBar.Menu.Title"] = "Announcement Bar",
                ["Plugins.Widgets.AnnouncementBar.Menu.Configure"] = "Manage Announcements",

                ["Plugins.Widgets.AnnouncementBar.Fields.Text"] = "Text",
                ["Plugins.Widgets.AnnouncementBar.Fields.Color"] = "Color",
                ["Plugins.Widgets.AnnouncementBar.Fields.DisplayOrder"] = "Display Order",
                ["Plugins.Widgets.AnnouncementBar.Fields.IsActive"] = "Is Active",
                ["Plugins.Widgets.AnnouncementBar.Fields.Actions"] = "Actions",

                ["Plugins.Widgets.AnnouncementBar.Button.AddNew"] = "Yeni Duyuru Ekle",
                ["Plugins.Widgets.AnnouncementBar.Button.BackToList"] = "Geri Dön",

                ["Plugins.Widgets.AnnouncementBar.Card.List"] = "Duyuru Listesi",
                ["Plugins.Widgets.AnnouncementBar.Card.Info"] = "Duyuru Bilgileri",

                ["Plugins.Widgets.AnnouncementBar.PageTitle.Create"] = "Yeni Duyuru",
                ["Plugins.Widgets.AnnouncementBar.PageTitle.Edit"] = "Duyuru Düzenle",

                ["Plugins.Widgets.AnnouncementBar.Validation.TextRequired"] = "Text alanı zorunludur.",
                ["Plugins.Widgets.AnnouncementBar.Validation.DisplayOrderInvalid"] = "Display Order alanı geçerli bir sayı olmalıdır.",

                ["Plugins.Widgets.AnnouncementBar.Boolean.Yes"] = "Evet",
                ["Plugins.Widgets.AnnouncementBar.Boolean.No"] = "Hayır"
            });

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.AnnouncementBar");
            await base.UninstallAsync();
        }

        #endregion

        #region Widget

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

        #endregion

        #region Admin Menu

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