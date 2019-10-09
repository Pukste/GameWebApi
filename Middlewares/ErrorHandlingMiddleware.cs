using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Globalization;
using System;

namespace gamewebapi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;
 
        public void CustomExceptionHandlerMiddleware(RequestDelegate next){
 
            _next = next;
        }
	
        public async Task Invoke(HttpContext context)
        {
	        try
            {
                await _next(context);
	        }
	        catch(Exception e)
            {
                context.Response.StatusCode = 404;
                throw new NotFoundException();
	        }
	    }
    }
}