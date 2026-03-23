using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.AnnouncementBar.Models
{
    public record ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("Plugins.Widgets.AnnouncementBar.Settings.Fields.IsEnabled")]
        public bool IsEnabled { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.AnnouncementBar.Settings.Fields.BackgroundColor")]
        public string BackgroundColor { get; set; }
    }
}