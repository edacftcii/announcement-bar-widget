using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.AnnouncementBar.Models;
using Nop.Plugin.Widgets.AnnouncementBar.Services;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.AnnouncementBar.Components
{
    public class AnnouncementBarViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IAnnouncementItemService _announcementItemService;

        #endregion

        #region Ctor

        public AnnouncementBarViewComponent(IAnnouncementItemService announcementItemService)
        {
            _announcementItemService = announcementItemService;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (string.IsNullOrWhiteSpace(widgetZone) || widgetZone != PublicWidgetZones.HomepageTop)
                return Content(string.Empty);

            if (additionalData != null)
                ViewData["AdditionalData"] = additionalData;

            var activeItems = await _announcementItemService.GetAllActiveAsync();

            if (activeItems == null || activeItems.Count == 0)
                return Content(string.Empty);

            var model = new AnnouncementBarModel();

            foreach (var item in activeItems)
            {
                var color = item.Color?.Trim();

                if (string.IsNullOrWhiteSpace(color))
                    color = "#ffffff";

                model.Items.Add(new AnnouncementBarItemModel
                {
                    Text = item.Text,
                    Color = color
                });
            }

            return View("~/Plugins/Widgets.AnnouncementBar/Views/Shared/Components/AnnouncementBar/Default.cshtml", model);
        }

        #endregion
    }
}