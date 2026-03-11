using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Data;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;
using Nop.Plugin.Widgets.AnnouncementBar.Models;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.AnnouncementBar.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class AnnouncementBarController : BasePluginController
    {
        #region Fields

        private readonly IRepository<AnnouncementItem> _announcementItemRepository;

        #endregion

        #region Ctor

        public AnnouncementBarController(IRepository<AnnouncementItem> announcementItemRepository)
        {
            _announcementItemRepository = announcementItemRepository;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Configure()
        {
            var items = await _announcementItemRepository.GetAllAsync(query =>
                query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id));

            var model = items.Select(x => new AnnouncementItemModel
            {
                Id = x.Id,
                Text = x.Text,
                Color = x.Color,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive
            }).ToList();

            return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/Configure.cshtml", model);
        }

        public IActionResult Create()
        {
            var model = new AnnouncementItemModel
            {
                IsActive = true,
                DisplayOrder = 1
            };

            return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
        }

        [HttpPost]
public async Task<IActionResult> CreatePost()
{
    var text = Request.Form["Text"].ToString();
    var color = Request.Form["Color"].ToString();
    var displayOrderValue = Request.Form["DisplayOrder"].ToString();
    var isActiveValue = Request.Form["IsActive"].ToString();

    int.TryParse(displayOrderValue, out var displayOrder);
    var isActive = isActiveValue.Contains("true") || isActiveValue.Contains("on");

    var model = new AnnouncementItemModel
    {
        Text = text,
        Color = color,
        DisplayOrder = displayOrder,
        IsActive = isActive
    };

    if (string.IsNullOrWhiteSpace(text))
    {
        ModelState.AddModelError(nameof(model.Text), "Text alanı zorunludur.");
        return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
    }

    var entity = new AnnouncementItem
    {
        Text = text,
        Color = color,
        DisplayOrder = displayOrder,
        IsActive = isActive
    };

    await _announcementItemRepository.InsertAsync(entity);

    return RedirectToAction(nameof(Configure));
}

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _announcementItemRepository.GetByIdAsync(id);

            if (entity == null)
                return RedirectToAction(nameof(Configure));

            var model = new AnnouncementItemModel
            {
                Id = entity.Id,
                Text = entity.Text,
                Color = entity.Color,
                DisplayOrder = entity.DisplayOrder,
                IsActive = entity.IsActive
            };

            return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
        }

        [HttpPost]
public async Task<IActionResult> EditPost()
{
    var idValue = Request.Form["Id"].ToString();
    var text = Request.Form["Text"].ToString();
    var color = Request.Form["Color"].ToString();
    var displayOrderValue = Request.Form["DisplayOrder"].ToString();
    var isActiveValue = Request.Form["IsActive"].ToString();

    int.TryParse(idValue, out var id);
    int.TryParse(displayOrderValue, out var displayOrder);
    var isActive = isActiveValue.Contains("true") || isActiveValue.Contains("on");

    var model = new AnnouncementItemModel
    {
        Id = id,
        Text = text,
        Color = color,
        DisplayOrder = displayOrder,
        IsActive = isActive
    };

    if (string.IsNullOrWhiteSpace(text))
    {
        ModelState.AddModelError(nameof(model.Text), "Text alanı zorunludur.");
        return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
    }

    var entity = await _announcementItemRepository.GetByIdAsync(id);

    if (entity == null)
        return RedirectToAction(nameof(Configure));

    entity.Text = text;
    entity.Color = color;
    entity.DisplayOrder = displayOrder;
    entity.IsActive = isActive;

    await _announcementItemRepository.UpdateAsync(entity);

    return RedirectToAction(nameof(Configure));
}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _announcementItemRepository.GetByIdAsync(id);

            if (entity != null)
                await _announcementItemRepository.DeleteAsync(entity);

            return RedirectToAction(nameof(Configure));
        }

        #endregion
    }
}