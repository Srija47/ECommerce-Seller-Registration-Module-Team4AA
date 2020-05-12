
using SellerService.Entities;
using SellerService.Models;
using SellerService.Repositories;
using System;
using System.Threading.Tasks;

namespace SellerService.Manager
{
    public class SellerManager : ISellerManager
    {
        private readonly ISellerRepository _iSellerRepository;

        public SellerManager(ISellerRepository iSellerRepository)
        {
            _iSellerRepository = iSellerRepository;
        }

        public async Task<bool> UpdateSellerProfile(SellerProfile seller)
        {
            bool user = await _iSellerRepository.UpdateSellerProfile(seller);

            return user;
        }

        public async Task<SellerProfile> ViewSellerProfile(int sellerid)
        {
            SellerProfile seller= await _iSellerRepository.ViewSellerProfile(sellerid);
            return seller;
        }
    }
}
