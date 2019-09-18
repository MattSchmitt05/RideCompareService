using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
                await HandleException(httpContext.Response, e);
            }
        }

        private static async Task HandleException(HttpResponse httpResponse, Exception exception)
        {
            var clientResponseWithExceptionDetails = new ClientResponseResource();

            switch (exception)
            {
                case RideCompareBusinessBaseException _:
                    httpResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpResponse.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Business-Type Exception";
                    clientResponseWithExceptionDetails.ResultCode = ErrorType.Business;
                    var innerExceptions = GetInnerExceptionMessages(exception);
                    clientResponseWithExceptionDetails.ResultMessage = string.Join(" ", innerExceptions);
                    break;
                default:
                    httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpResponse.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Technical-Type Exception";
                    clientResponseWithExceptionDetails.ResultCode = ErrorType.Technical;
                    clientResponseWithExceptionDetails.ResultMessage = "There was a technical issue either in the Domain Service or in one of the external services. Please try again.";
                    break;
            }

            httpResponse.HttpContext.Response.Headers.Add("ExceptionType", exception.GetType().Name);
            httpResponse.HttpContext.Response.ContentType = "application/json";

            var clientResponseJsonString = JsonConvert.SerializeObject(clientResponseWithExceptionDetails);

            await httpResponse.WriteAsync(clientResponseJsonString).ConfigureAwait(false);
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
