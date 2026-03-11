using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Data;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;

namespace Nop.Plugin.Widgets.AnnouncementBar.Services
{
    public class AnnouncementItemService : IAnnouncementItemService
    {
        #region Fields

        private readonly IRepository<AnnouncementItem> _announcementItemRepository;

        #endregion

        #region Ctor

        public AnnouncementItemService(IRepository<AnnouncementItem> announcementItemRepository)
        {
            _announcementItemRepository = announcementItemRepository;
        }

        #endregion

        #region Methods

        public async Task<IList<AnnouncementItem>> GetAllAsync()
        {
            return await _announcementItemRepository.GetAllAsync(query =>
                query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id));
        }

        public async Task<IList<AnnouncementItem>> GetAllActiveAsync()
        {
            return await _announcementItemRepository.GetAllAsync(query =>
                query.Where(x => x.IsActive)
                     .OrderBy(x => x.DisplayOrder)
                     .ThenBy(x => x.Id));
        }

        public async Task<AnnouncementItem> GetByIdAsync(int id)
        {
            return await _announcementItemRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(AnnouncementItem item)
        {
            await _announcementItemRepository.InsertAsync(item);
        }

        public async Task UpdateAsync(AnnouncementItem item)
        {
            await _announcementItemRepository.UpdateAsync(item);
        }

        public async Task DeleteAsync(AnnouncementItem item)
        {
            await _announcementItemRepository.DeleteAsync(item);
        }

        #endregion
    }
}