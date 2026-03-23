using System.Collections.Generic;

namespace Nop.Plugin.Widgets.AnnouncementBar.Models
{
    public class AnnouncementBarItemModel
    {
        #region Properties

        public string Text { get; set; }

        public string Color { get; set; }

        public string LinkUrl { get; set; }

        #endregion
    }

    public class AnnouncementBarModel
    {
        #region Properties

        public string BackgroundColor { get; set; }

        public IList<AnnouncementBarItemModel> Items { get; set; } = new List<AnnouncementBarItemModel>();


        #endregion
    }
}