
using AutoMapper;
using SellerService.Entities;
using SellerService.Models;
using System.Threading.Tasks;

namespace SellerService.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly SellerDBContext _context;
        public SellerRepository(SellerDBContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateSellerProfile(SellerProfile seller)
        {
            Seller seller1 = _context.Seller.Find(seller.Sellerid);
            if (seller != null)
            {

                seller1.Username = seller.Username;
                seller1.Password = seller.Password;
                seller1.Companyname = seller.Companyname;
                seller1.Briefaboutcompany = seller.Briefaboutcompany;
                seller1.Postaladdress = seller.Postaladdress;
                seller1.Website = seller.Website;
                seller1.Emailid = seller.Emailid;
                seller1.Contactnumber = seller.Contactnumber;

            };
            _context.Update(seller1);
            var sellerId = await _context.SaveChangesAsync();
            if (sellerId > 0)
                return true;
            else
                return false;
        }

        public async Task<SellerProfile> ViewSellerProfile(int sellerid)
        {
            Seller seller = await _context.Seller.FindAsync(sellerid);
            SellerProfile sellerProfile = new SellerProfile();
            if (seller != null)
            {

                sellerProfile.Sellerid = seller.Sellerid;
                sellerProfile.Username = seller.Username;
                sellerProfile.Password = seller.Password;
                sellerProfile.Companyname = seller.Companyname;
                sellerProfile.Briefaboutcompany = seller.Briefaboutcompany;
                sellerProfile.Postaladdress = seller.Postaladdress;
                sellerProfile.Website = seller.Website;
                sellerProfile.Emailid = seller.Emailid;
                sellerProfile.Contactnumber = seller.Contactnumber;
            }
            return sellerProfile;
        }
    }
}