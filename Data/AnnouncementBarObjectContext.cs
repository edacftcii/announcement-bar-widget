using LinqToDB;
using LinqToDB.Data;
using Nop.Data;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;

namespace Nop.Plugin.Widgets.AnnouncementBar.Data
{
    public class AnnouncementBarObjectContext : DataConnection, INopDataProvider
    {
        #region Ctor

        public AnnouncementBarObjectContext(IDataProvider dataProvider, DataOptions options)
            : base(dataProvider, options.Options)
        {
        }

        #endregion

        #region Utilities

        public ITable<AnnouncementItem> AnnouncementItems => GetTable<AnnouncementItem>();

        #endregion

        #region Methods

        public static string Name => "AnnouncementBarObjectContext";

        public static void CreateTable()
        {
            var builder = new AnnouncementItemBuilder();
            var nameCompatibilityManager = new NameCompatibilityManager(TableNameCompatibility.NO_COMPATIBILITY, null);
            builder.CreateTable(null, nameCompatibilityManager);
        }

        #endregion
    }
}