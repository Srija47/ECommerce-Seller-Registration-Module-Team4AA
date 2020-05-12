using AccountService.Entities;
using AccountService.Manager;
using AccountService.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AccountService.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SellerDBContext _context;
        public AccountRepository(SellerDBContext context)
        {
            _context = context;

        }

        /// <summary>
        /// To Add new seller to Seller table in SellerDb database
        /// </summary>
        /// <param name="seller"></param>
        /// <returns></returns>
        public async Task<bool> SellerRegisterAsync(SellerRegister seller)
        {
            Seller seller1 = new Seller();
            if (seller!=null)
            {
                seller1.Username = seller.Username;
                seller1.Password = seller.Password;
                seller1.Gst = seller.Gst;
                seller1.Companyname = seller.Companyname;
                seller1.Briefaboutcompany =seller.Briefaboutcompany;
                seller1.Postaladdress = seller.Postaladdress;
                seller1.Website = seller.Website;
                seller1.Emailid = seller.Emailid;
                seller1.Contactnumber = seller.Contactnumber;

            };
            
            if((seller1.Username!=seller.Username)&&(seller1.Emailid!=seller.Emailid))
            {
                _context.Add(seller1);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// To Check Paticular user is there or not
        /// </summary>
        /// <param name="uname"></param> 
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<SellerLogin> ValidateSellerAsync(string uname, string pwd)
        {
           var user= await _context.Seller.SingleOrDefaultAsync(e => e.Username == uname && e.Password == pwd);
            if (user!= null)
            {
                return new SellerLogin
                {

                    Username = user.Username,
                    Password = user.Password,
                };
            }
            return null;

        }
    }
}
