using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;

namespace Nop.Plugin.Widgets.AnnouncementBar.Data
{
    [NopMigration("2026/03/19 10:00:00:0000000", "Add LinkUrl to AnnouncementItem", MigrationProcessType.Update)]
    public class AddLinkUrlToAnnouncementItem : MigrationBase
    {
        public override void Up()
        {
            if (!Schema.Table(nameof(AnnouncementItem)).Column(nameof(AnnouncementItem.LinkUrl)).Exists())
            {
                Alter.Table(nameof(AnnouncementItem))
                    .AddColumn(nameof(AnnouncementItem.LinkUrl)).AsString(1000).Nullable();
            }
        }

        public override void Down()
        {
        }
    }
}