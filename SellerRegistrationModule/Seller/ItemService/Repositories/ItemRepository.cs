using ItemService.Entities;
using ItemService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemService.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly SellerDBContext _context;
        public ItemRepository(SellerDBContext context)
        {
            _context = context;
        }
        public async Task<bool> AddItems(ItemsModel items)
        {
            Items items1 = new Items();
            if (items != null)
            {
                items1.Itemname = items.Itemname;
                items1.Categoryid = items.Categoryid;
                items1.Subcategoryid = items.Subcategoryid;
                items1.Price = items.Price;
                items1.Description = items.Description;
                items1.Stocknumber = items.Stocknumber;
                items1.Remarks = items.Remarks;
                items1.Imagename = items.Imagename;
                items1.Sellerid = items.Sellerid;

            }
            _context.Add(items1);
            var item = await _context.SaveChangesAsync();
            if (item > 0)
                return true;
            else
                return false;
        }

        public void DeleteItems(ItemsModel itemid)
        {
            Items i = _context.Items.Find(itemid);
            _context.Remove(i);
            _context.SaveChanges();

        }
        public async Task<bool> UpdateItems(ItemsModel itemsmodel)
        {
            Items items1 = _context.Items.Find(itemsmodel.Itemid);
            if (itemsmodel != null)
            {

                items1.Itemname = itemsmodel.Itemname;
                items1.Categoryid = itemsmodel.Categoryid;
                items1.Subcategoryid = itemsmodel.Subcategoryid;
                items1.Price = itemsmodel.Price;
                items1.Description = itemsmodel.Description;
                items1.Stocknumber = itemsmodel.Stocknumber;
                items1.Remarks = itemsmodel.Remarks;
                items1.Imagename = itemsmodel.Imagename;
                items1.Sellerid = itemsmodel.Sellerid;

            };
            _context.Update(items1);
            var sellerId = await _context.SaveChangesAsync();
            if (sellerId > 0)
                return true;
            else
                return false;
        }


        public List<ItemsModel> ViewItems(int sellerid)
        {
            List<ItemsModel> items1 = new List<ItemsModel>();

            List<Items> items = _context.Items.Where(e => e.Sellerid == sellerid).ToList();
            foreach (Items i in items)
            {
                ItemsModel b = new ItemsModel();
                b.Itemid = i.Itemid;
                b.Itemname = i.Itemname;
                b.Price = i.Price;
                b.Remarks = i.Remarks;
                b.Stocknumber = i.Stocknumber;
                b.Description = i.Description;
                b.Sellerid = i.Sellerid;
                items1.Add(b);

            }
            return items1;
        }
    }

}
