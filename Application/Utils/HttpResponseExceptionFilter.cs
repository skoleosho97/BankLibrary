using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Utils
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BaseException exception)
            {
                context.Result = new ObjectResult(exception.Error)
                {
                    StatusCode = exception.StatusCode,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
