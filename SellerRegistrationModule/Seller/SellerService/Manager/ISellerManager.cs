

using SellerService.Models;
using System.Threading.Tasks;

namespace SellerService.Manager
{
    public interface ISellerManager
    {
        Task<bool> UpdateSellerProfile(SellerProfile seller);
        Task<SellerProfile> ViewSellerProfile(int sellerid);
    }
}
