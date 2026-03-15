using System.Threading.Tasks;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;
using Nop.Plugin.Widgets.AnnouncementBar.Models;

namespace Nop.Plugin.Widgets.AnnouncementBar.Factories
{
    public interface IAnnouncementModelFactory
    {
        #region Methods

        Task<AnnouncementItemSearchModel> PrepareSearchModelAsync(AnnouncementItemSearchModel searchModel);

        Task<AnnouncementItemListModel> PrepareListModelAsync(AnnouncementItemSearchModel searchModel);

        Task<AnnouncementItemModel> PrepareModelAsync(AnnouncementItem entity);

        Task<AnnouncementItem> PrepareEntityAsync(AnnouncementItemModel model, AnnouncementItem entity = null);

        #endregion
    }
}