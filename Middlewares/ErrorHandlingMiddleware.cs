using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;
using System;

namespace gamewebapi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
 
        public ErrorHandlingMiddleware(RequestDelegate next){
 
            _next = next;
        }
	
        public async Task InvokeAsync(HttpContext context)
        {
	        try
            {
                await _next(context);
	        }
	        catch(NotFoundException)
            {
                context.Response.StatusCode = 404;
	        }
            catch(RequirementException)
            {
                context.Response.StatusCode = 400;
            }
	    }
    }
}