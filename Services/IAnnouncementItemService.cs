using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;

namespace Nop.Plugin.Widgets.AnnouncementBar.Services
{
    public interface IAnnouncementItemService
    {
        #region Methods

        Task<IList<AnnouncementItem>> GetAllAsync();

        Task<IList<AnnouncementItem>> GetAllActiveAsync();

        Task<AnnouncementItem> GetByIdAsync(int id);

        Task InsertAsync(AnnouncementItem item);

        Task UpdateAsync(AnnouncementItem item);

        Task DeleteAsync(AnnouncementItem item);

        #endregion
    }
}