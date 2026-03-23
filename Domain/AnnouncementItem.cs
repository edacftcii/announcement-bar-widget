using Nop.Core;

namespace Nop.Plugin.Widgets.AnnouncementBar.Domain
{
    public class AnnouncementItem : BaseEntity
    {
        public string Text { get; set; }

        public string Color { get; set; }

        public string LinkUrl { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}