using AccountService.Entities;
using AccountService.Models;
using AccountService.Repositories;
using System;
using System.Threading.Tasks;

namespace AccountService.Manager
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository _iAccountRepository;

        public AccountManager(IAccountRepository iAccountRepository)
        {
            _iAccountRepository = iAccountRepository;
        }

        public async Task<bool> SellerRegister(SellerRegister seller)
        {
           bool user = await _iAccountRepository.SellerRegisterAsync(seller);
           return user;
        }

        public async Task<SellerLogin> ValidateSeller(string uname, string pwd)
        {
            var seller1 = await _iAccountRepository.ValidateSellerAsync(uname,pwd);
            if (seller1.Username == uname && seller1.Password == pwd)
            {
                return seller1;
            }
            else
            {
                Console.WriteLine("Invalid");
                return null;
            }

        }
    }
}
