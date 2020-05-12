using ItemService.Entities;
using ItemService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemService.Repositories
{
    public interface IItemRepository
    {
        Task<bool> AddItems(ItemsModel items);
        public void DeleteItems(ItemsModel itemid);
        List<ItemsModel> ViewItems(int sellerid);
        Task<bool> UpdateItems(ItemsModel itemsmodel);
    }
}
