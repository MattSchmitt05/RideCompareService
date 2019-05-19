using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RideCompareService.Controllers.ResourceModels;
using RideCompareService.DomainLayer.Constants;
using RideCompareService.DomainLayer.Exceptions;

namespace RideCompareService
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    [ExcludeFromCodeCoverage]
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                return;
            }
            catch (Exception e)
            {
                var clientResponseWithExceptionDetails = new ClientResponseResource();

                switch (e)
                {
                    case RideCompareBusinessBaseException _:
                        httpContext.Response.StatusCode = 400;
                        clientResponseWithExceptionDetails.ResultCode = ErrorType.Business;
                        var innerExceptions = GetInnerExceptionMessages(e);
                        clientResponseWithExceptionDetails.ResultMessage = string.Join(" ", innerExceptions);
                        break;
                    default:
                        httpContext.Response.StatusCode = 500;
                        clientResponseWithExceptionDetails.ResultCode = ErrorType.Technical;
                        clientResponseWithExceptionDetails.ResultMessage = "There was a technical issue either in the Domain Service or in one of the external services. Please try again.";
                        break;
                }

                httpContext.Response.Headers.Add("ExceptionType", e.GetType().Name);
                httpContext.Response.ContentType = "application/json";
                
                var clientResponseJsonString = JsonConvert.SerializeObject(clientResponseWithExceptionDetails);

                await httpContext.Response.WriteAsync(clientResponseJsonString);
            }
        }

        private static IEnumerable<string> GetInnerExceptionMessages(Exception exception)
        {
            do
            {
                yield return exception.Message;
                exception = exception.InnerException;
            } while (exception != null);
        }
    }
}
