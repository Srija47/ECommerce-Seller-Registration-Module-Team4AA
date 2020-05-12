using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountService.GlobalExceptionFilter
{
    public interface IExceptionFilter:IFilterMetadata
    {
        void OnException(ExceptionContext context);
    }
}
