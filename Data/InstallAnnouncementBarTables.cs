using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;

namespace Nop.Plugin.Widgets.AnnouncementBar.Data
{
    [NopMigration("2026/03/09 10:00:00:0000000", "Widgets.AnnouncementBar schema", MigrationProcessType.Installation)]
    public class InstallAnnouncementBarTables : MigrationBase
    {
        #region Methods

        public override void Up()
        {
            if (!Schema.Table(nameof(AnnouncementItem)).Exists())
                Create.TableFor<AnnouncementItem>();
        }

         public override void Down()
        {
            
        }

        #endregion
    }
}