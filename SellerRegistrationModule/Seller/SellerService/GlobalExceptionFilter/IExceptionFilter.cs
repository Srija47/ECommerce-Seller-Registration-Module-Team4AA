using Microsoft.AspNetCore.Mvc.Filters;

namespace SellerService.GlobalExceptionFilter
{
    public interface IExceptionFilter:IFilterMetadata
    {
        void OnException(ExceptionContext context);
    }
}
