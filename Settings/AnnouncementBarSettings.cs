using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.AnnouncementBar.Settings
{
    public class AnnouncementBarSettings : ISettings
    {
        public bool IsEnabled { get; set; }

        public string BackgroundColor { get; set; }
    }
}