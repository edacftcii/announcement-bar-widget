using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Data;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;
using Nop.Plugin.Widgets.AnnouncementBar.Models;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.AnnouncementBar.Components
{
    public class AnnouncementBarViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IRepository<AnnouncementItem> _announcementItemRepository;

        #endregion

        #region Ctor

        public AnnouncementBarViewComponent(IRepository<AnnouncementItem> announcementItemRepository)
        {
            _announcementItemRepository = announcementItemRepository;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var items = await _announcementItemRepository.GetAllAsync(query =>
                query.Where(x => x.IsActive)
                     .OrderBy(x => x.DisplayOrder)
                     .ThenBy(x => x.Id));

            var activeItems = items.ToList();

            if (!activeItems.Any())
                return Content(string.Empty);

            var model = new AnnouncementBarModel();

            foreach (var item in activeItems)
            {
                model.Items.Add(new AnnouncementBarItemModel
                {
                    Text = item.Text,
                    Color = string.IsNullOrWhiteSpace(item.Color) ? "#ffffff" : item.Color
                });
            }

            return View("~/Plugins/Widgets.AnnouncementBar/Views/Shared/Components/AnnouncementBar/Default.cshtml", model);
        }

        #endregion
    }
}