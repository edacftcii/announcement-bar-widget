using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.AnnouncementBar.Models
{
    public record AnnouncementItemModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugins.Widgets.AnnouncementBar.Fields.Text")]
        public string Text { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.AnnouncementBar.Fields.Color")]
        public string Color { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.AnnouncementBar.Fields.LinkUrl")]
        public string LinkUrl { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.AnnouncementBar.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.AnnouncementBar.Fields.IsActive")]
        public bool IsActive { get; set; }
    }
}