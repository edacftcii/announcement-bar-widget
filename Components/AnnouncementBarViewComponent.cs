using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.AnnouncementBar.Models;
using Nop.Plugin.Widgets.AnnouncementBar.Services;
using Nop.Plugin.Widgets.AnnouncementBar.Settings;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.AnnouncementBar.Components
{
    public class AnnouncementBarViewComponent : NopViewComponent
    {
        private readonly IAnnouncementItemService _announcementItemService;
        private readonly ISettingService _settingService;

        public AnnouncementBarViewComponent(
            IAnnouncementItemService announcementItemService,
            ISettingService settingService)
        {
            _announcementItemService = announcementItemService;
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (string.IsNullOrWhiteSpace(widgetZone) || widgetZone != PublicWidgetZones.HomepageTop)
                return Content(string.Empty);

            var settings = await _settingService.LoadSettingAsync<AnnouncementBarSettings>();

            if (!settings.IsEnabled)
                return Content(string.Empty);

            var activeItems = await _announcementItemService.GetAllActiveAsync();

            if (activeItems == null || activeItems.Count == 0)
                return Content(string.Empty);

            var model = new AnnouncementBarModel
            {
                BackgroundColor = string.IsNullOrWhiteSpace(settings.BackgroundColor)
                    ? "#111111"
                    : settings.BackgroundColor.Trim()
            };

            foreach (var item in activeItems)
            {
                var color = string.IsNullOrWhiteSpace(item.Color)
                    ? "#ffffff"
                    : item.Color.Trim();

                model.Items.Add(new AnnouncementBarItemModel
                {
                    Text = item.Text,
                    Color = color,
                    LinkUrl = item.LinkUrl
                });
            }

            return View("~/Plugins/Widgets.AnnouncementBar/Views/Shared/Components/AnnouncementBar/Default.cshtml", model);
        }
    }
}