
using SellerService.Models;
using System.Threading.Tasks;

namespace SellerService.Repositories
{
    public interface ISellerRepository
    {
        Task<bool> UpdateSellerProfile(SellerProfile seller);
        Task<SellerProfile> ViewSellerProfile(int sellerid);
    }
}
