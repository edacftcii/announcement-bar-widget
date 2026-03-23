using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.AnnouncementBar.Factories;
using Nop.Plugin.Widgets.AnnouncementBar.Models;
using Nop.Plugin.Widgets.AnnouncementBar.Services;
using Nop.Plugin.Widgets.AnnouncementBar.Settings;
using Nop.Services.Configuration;
using Nop.Services.Localization;
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

        private readonly IAnnouncementItemService _announcementItemService;
        private readonly IAnnouncementModelFactory _announcementModelFactory;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        public AnnouncementBarController(
            IAnnouncementItemService announcementItemService,
            IAnnouncementModelFactory announcementModelFactory,
            ILocalizationService localizationService,
            ISettingService settingService)
        {
            _announcementItemService = announcementItemService;
            _announcementModelFactory = announcementModelFactory;
            _localizationService = localizationService;
            _settingService = settingService;
        }

        #endregion

        #region Methods

   public async Task<IActionResult> Configure()
{
    var settings = await _settingService.LoadSettingAsync<AnnouncementBarSettings>();

    var model = new ConfigurationModel
    {
        IsEnabled = settings.IsEnabled,
        BackgroundColor = settings.BackgroundColor
    };

    return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/Configure.cshtml", model);
}

[HttpPost]
public async Task<IActionResult> Configure(ConfigurationModel model)
{
    if (!ModelState.IsValid)
        return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/Configure.cshtml", model);

    var settings = await _settingService.LoadSettingAsync<AnnouncementBarSettings>();

    settings.IsEnabled = model.IsEnabled;
    settings.BackgroundColor = model.BackgroundColor;

    await _settingService.SaveSettingAsync(settings);
    await _settingService.ClearCacheAsync();

    var savedSettings = await _settingService.LoadSettingAsync<AnnouncementBarSettings>();

    var resultModel = new ConfigurationModel
    {
        IsEnabled = savedSettings.IsEnabled,
        BackgroundColor = savedSettings.BackgroundColor
    };

    return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/Configure.cshtml", resultModel);
}


        public async Task<IActionResult> Manage()
        {
            var searchModel = await _announcementModelFactory.PrepareSearchModelAsync(new AnnouncementItemSearchModel());
            return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/List.cshtml", searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(AnnouncementItemSearchModel searchModel)
        {
            var model = await _announcementModelFactory.PrepareListModelAsync(searchModel);
            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _announcementModelFactory.PrepareModelAsync(null);
            return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
        }

        [HttpPost]
public async Task<IActionResult> CreatePost(AnnouncementItemModel model)
{
    // 🔹 Link temizleme
    if (!string.IsNullOrWhiteSpace(model.LinkUrl))
        model.LinkUrl = model.LinkUrl.Trim();

    // 🔹 Basit URL kontrolü (opsiyonel ama önerilir)
    if (!string.IsNullOrWhiteSpace(model.LinkUrl) &&
        !Uri.IsWellFormedUriString(model.LinkUrl, UriKind.Absolute))
    {
        ModelState.AddModelError(
            nameof(model.LinkUrl),
            "Geçerli bir URL giriniz (https://...)");
    }

    if (!ModelState.IsValid)
        return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);

    if (string.IsNullOrWhiteSpace(model.Text))
    {
        ModelState.AddModelError(
            nameof(model.Text),
            await _localizationService.GetResourceAsync("Plugins.Widgets.AnnouncementBar.Validation.TextRequired"));

        return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
    }

    if (model.DisplayOrder < 0)
    {
        ModelState.AddModelError(
            nameof(model.DisplayOrder),
            await _localizationService.GetResourceAsync("Plugins.Widgets.AnnouncementBar.Validation.DisplayOrderInvalid"));

        return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
    }

    var entity = await _announcementModelFactory.PrepareEntityAsync(model);

    await _announcementItemService.InsertAsync(entity);

    return RedirectToAction(nameof(Manage));
}

    public async Task<IActionResult> Edit(int id)
{
    var entity = await _announcementItemService.GetByIdAsync(id);

    if (entity == null)
        return RedirectToAction(nameof(Manage));

    var model = new AnnouncementItemModel
    {
        
        Id = entity.Id,
        Text = entity.Text,
        Color = entity.Color,
        LinkUrl = entity.LinkUrl,
        DisplayOrder = entity.DisplayOrder,
        IsActive = entity.IsActive
    };

    return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
}

        [HttpPost]
public async Task<IActionResult> EditPost(AnnouncementItemModel model)
{
    if (model.Id <= 0)
        return RedirectToAction(nameof(Manage));

    // 🔹 Link temizleme
    if (!string.IsNullOrWhiteSpace(model.LinkUrl))
        model.LinkUrl = model.LinkUrl.Trim();

    // 🔹 URL kontrolü
    if (!string.IsNullOrWhiteSpace(model.LinkUrl) &&
        !Uri.IsWellFormedUriString(model.LinkUrl, UriKind.Absolute))
    {
        ModelState.AddModelError(
            nameof(model.LinkUrl),
            "Geçerli bir URL giriniz (https://...)");
    }

    if (!ModelState.IsValid)
        return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);

    if (string.IsNullOrWhiteSpace(model.Text))
    {
        ModelState.AddModelError(
            nameof(model.Text),
            await _localizationService.GetResourceAsync("Plugins.Widgets.AnnouncementBar.Validation.TextRequired"));

        return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
    }

    if (model.DisplayOrder < 0)
    {
        ModelState.AddModelError(
            nameof(model.DisplayOrder),
            await _localizationService.GetResourceAsync("Plugins.Widgets.AnnouncementBar.Validation.DisplayOrderInvalid"));

        return View("~/Plugins/Widgets.AnnouncementBar/Views/Configure/CreateOrEdit.cshtml", model);
    }

    var entity = await _announcementItemService.GetByIdAsync(model.Id);

    if (entity == null)
        return RedirectToAction(nameof(Manage));

    entity = await _announcementModelFactory.PrepareEntityAsync(model, entity);

    await _announcementItemService.UpdateAsync(entity);

    return RedirectToAction(nameof(Manage));
}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return Json(new { Result = false });

            var entity = await _announcementItemService.GetByIdAsync(id);

            if (entity == null)
                return Json(new { Result = false });

            await _announcementItemService.DeleteAsync(entity);

            return Json(new { Result = true });
        }

        #endregion
    }
}