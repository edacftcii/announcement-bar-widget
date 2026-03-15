using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.AnnouncementBar.Domain;
using Nop.Plugin.Widgets.AnnouncementBar.Models;
using Nop.Plugin.Widgets.AnnouncementBar.Services;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Widgets.AnnouncementBar.Factories
{
    public class AnnouncementModelFactory : IAnnouncementModelFactory
    {
        #region Fields

        private readonly IAnnouncementItemService _announcementItemService;

        #endregion

        #region Ctor

        public AnnouncementModelFactory(IAnnouncementItemService announcementItemService)
        {
            _announcementItemService = announcementItemService;
        }

        #endregion

        #region Methods

        public virtual Task<AnnouncementItemSearchModel> PrepareSearchModelAsync(AnnouncementItemSearchModel searchModel)
        {
            searchModel ??= new AnnouncementItemSearchModel();

            searchModel.SetGridPageSize();

            return Task.FromResult(searchModel);
        }

        public virtual async Task<AnnouncementItemListModel> PrepareListModelAsync(AnnouncementItemSearchModel searchModel)
        {
            var items = await _announcementItemService.GetAllAsync();
            var pagedItems = new PagedList<AnnouncementItem>(items, searchModel.Page - 1, searchModel.PageSize);

            var model = new AnnouncementItemListModel();

            model = await model.PrepareToGridAsync<AnnouncementItemListModel, AnnouncementItemModel, AnnouncementItem>(
                searchModel,
                pagedItems,
                () => pagedItems.Select(x => new AnnouncementItemModel
                {
                    Id = x.Id,
                    Text = x.Text,
                    Color = x.Color,
                    DisplayOrder = x.DisplayOrder,
                    IsActive = x.IsActive
                }).ToAsyncEnumerable());

            return model;
        }

        public virtual Task<AnnouncementItemModel> PrepareModelAsync(AnnouncementItem entity)
        {
            if (entity == null)
            {
                return Task.FromResult(new AnnouncementItemModel
                {
                    IsActive = true,
                    DisplayOrder = 1
                });
            }

            return Task.FromResult(new AnnouncementItemModel
            {
                Id = entity.Id,
                Text = entity.Text,
                Color = entity.Color,
                DisplayOrder = entity.DisplayOrder,
                IsActive = entity.IsActive
            });
        }

        public virtual Task<AnnouncementItem> PrepareEntityAsync(AnnouncementItemModel model, AnnouncementItem entity = null)
        {
            entity ??= new AnnouncementItem();

            entity.Text = model.Text;
            entity.Color = model.Color;
            entity.DisplayOrder = model.DisplayOrder;
            entity.IsActive = model.IsActive;

            return Task.FromResult(entity);
        }

        #endregion
    }
}