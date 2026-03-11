using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;

namespace Nop.Plugin.Widgets.AnnouncementBar.Data
{
    [NopMigration("2026/03/09 10:00:00:0000000", "Widgets.AnnouncementBar base schema", MigrationProcessType.Installation)]
    public class InstallAnnouncementBarTables : AutoReversingMigration
    {
        #region Methods

        public override void Up()
        {
            Create.TableFor<AnnouncementItem>();
        }

        #endregion
    }
}