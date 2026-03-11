using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;

namespace Nop.Plugin.Widgets.AnnouncementBar.Data
{
    public class AnnouncementItemBuilder : NopEntityBuilder<AnnouncementItem>
    {
        #region Methods

        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(AnnouncementItem.Text)).AsString(1000).NotNullable()
                .WithColumn(nameof(AnnouncementItem.Color)).AsString(50).Nullable()
                .WithColumn(nameof(AnnouncementItem.DisplayOrder)).AsInt32().NotNullable()
                .WithColumn(nameof(AnnouncementItem.IsActive)).AsBoolean().NotNullable();
        }

        #endregion
    }
}