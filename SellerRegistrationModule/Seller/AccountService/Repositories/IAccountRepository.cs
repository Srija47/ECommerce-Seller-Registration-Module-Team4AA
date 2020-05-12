using AccountService.Entities;
using AccountService.Models;
using System.Threading.Tasks;

//IUser Repository
namespace AccountService.Repositories
{
   public interface IAccountRepository
    {
        Task<SellerLogin> ValidateSellerAsync(string uname, string pwd);
        Task<bool> SellerRegisterAsync(SellerRegister seller);
    }
}
