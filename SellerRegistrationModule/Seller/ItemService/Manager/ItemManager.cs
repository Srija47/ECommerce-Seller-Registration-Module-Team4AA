using ItemService.Entities;
using ItemService.Models;
using ItemService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemService.Manager
{
    public class ItemManager : IItemManager
    {
        private readonly IItemRepository _repo;
        public ItemManager(IItemRepository repo)
        {
            _repo = repo;

        }

        public async Task<bool> AddItemsManager(ItemsModel items)
        {
            bool item = await _repo.AddItems(items);
            return item;
        }

        public void DeleteItemsManager(ItemsModel itemid)
        {
            _repo.DeleteItems(itemid);

        }

        public async Task<bool> UpdateItemsManager(ItemsModel itemsmodel)
        {
           bool items= await _repo.UpdateItems(itemsmodel);
            return items;
        }

        public List<ItemsModel> ViewItemsManager(int sellerid)
        {
            List<ItemsModel> items = _repo.ViewItems(sellerid);
            return items;
        }


    }

}
