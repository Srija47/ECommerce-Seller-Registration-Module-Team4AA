using ItemService.Entities;
using ItemService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemService.Manager
{
    public interface IItemManager
    {
        List<ItemsModel> ViewItemsManager(int sellerid);
        Task<bool> AddItemsManager(ItemsModel item);
        public void DeleteItemsManager(ItemsModel itemid);
        Task<bool> UpdateItemsManager(ItemsModel itemsmodel);
    }
}
