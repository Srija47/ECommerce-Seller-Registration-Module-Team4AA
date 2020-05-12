using Microsoft.AspNetCore.Mvc.Filters;

namespace ItemService.GlobalExceptionFilter
{
    public interface IExceptionFilter:IFilterMetadata
    {
        void OnException(ExceptionContext context);
    }
}
