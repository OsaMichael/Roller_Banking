using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository
{
  public  class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpAccessor;

        public CorrelationIdMiddleware(RequestDelegate next, IHttpContextAccessor httpAccessor)
        {
            _next = next;
            _httpAccessor = httpAccessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("X-Correlation-ID"))
            {
                context.TraceIdentifier = context.Request.Headers["X-Correlation-ID"];
                // WORKAROUND: On ASP.NET Core 2.2.1 we need to re-store in AsyncLocal the new TraceId, HttpContext Pair
                _httpAccessor.HttpContext = context;
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
