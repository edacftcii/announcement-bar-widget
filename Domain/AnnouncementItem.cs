using Nop.Core;

namespace Nop.Plugin.Widgets.AnnouncementBar.Domain
{
    public class AnnouncementItem : BaseEntity
    {
        #region Properties

        public string Text { get; set; }

        public string Color { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        #endregion
    }
}