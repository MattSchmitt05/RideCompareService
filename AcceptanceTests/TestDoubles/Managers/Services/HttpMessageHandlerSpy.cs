using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AcceptanceTests.TestMediators;
using Newtonsoft.Json;

namespace AcceptanceTests.TestDoubles.Managers.Services
{
    [ExcludeFromCodeCoverage]
    internal sealed class HttpMessageHandlerSpy : HttpMessageHandler
    {
        private const string ExceptionMessage = "When using the Test Mediator, you MUST set either the ResourceMock OR ResponseString.";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.GetLeftPart(UriPartial.Authority).Contains("lyft"))
            {
                return await GetMockedLyftServiceResponse(request);
            }

            if (request.RequestUri.GetLeftPart(UriPartial.Authority).Contains("uber"))
            {
                return await GetMockedUberServiceResponse(request);
            }

            throw new NotSupportedException("An unsupported url has been used. Please review appropriate service urls for this domain service.");
        }

        private async Task<HttpResponseMessage> GetMockedLyftServiceResponse(HttpRequestMessage request)
        {
            HttpResponseMessage httpResponseMessage = null;

            if (string.Compare(request.RequestUri.AbsolutePath, "/oauth/token", StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (TestMediatorForLyftGateway.LyftAccessTokenResourceMock == null &&
                    TestMediatorForLyftGateway.ResponseString == null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Null.");
                }

                if (TestMediatorForLyftGateway.LyftAccessTokenResourceMock != null &&
                    TestMediatorForLyftGateway.ResponseString != null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Not Null.");
                }

                httpResponseMessage = new HttpResponseMessage(TestMediatorForLyftGateway.HttpStatusCodeForLyftAccessToken)
                {
                    ReasonPhrase = TestMediatorForLyftGateway.ReasonPhrase,
                    Content = TestMediatorForLyftGateway.LyftAccessTokenResourceMock == null
                        ? new StringContent(TestMediatorForLyftGateway.ResponseString)
                        : new StringContent(JsonConvert.SerializeObject(TestMediatorForLyftGateway.LyftAccessTokenResourceMock), Encoding.UTF8, "application/json")
                };
                
                return await Task.FromResult(httpResponseMessage);
            }

            if (request.RequestUri.AbsolutePath.Contains("ridetypes"))
            {
                if (TestMediatorForLyftGateway.LyftRideTypeResourceMock == null &&
                    TestMediatorForLyftGateway.ResponseString == null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Null.");
                }

                if (TestMediatorForLyftGateway.LyftRideTypeResourceMock != null &&
                    TestMediatorForLyftGateway.ResponseString != null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Not Null.");
                }

                httpResponseMessage = new HttpResponseMessage(TestMediatorForLyftGateway.HttpStatusCodeForLyftRideTypes)
                {
                    ReasonPhrase = TestMediatorForLyftGateway.ReasonPhrase,
                    Content = TestMediatorForLyftGateway.LyftRideTypeResourceMock == null
                        ? new StringContent(TestMediatorForLyftGateway.ResponseString)
                        : new StringContent(JsonConvert.SerializeObject(TestMediatorForLyftGateway.LyftRideTypeResourceMock), Encoding.UTF8, "application/json")
                };
                
                return await Task.FromResult(httpResponseMessage);
            }

            if (request.RequestUri.AbsolutePath.Contains("cost"))
            {
                if (TestMediatorForLyftGateway.LyftRideCostEstimateResourceMock == null &&
                    TestMediatorForLyftGateway.ResponseString == null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Null.");
                }

                if (TestMediatorForLyftGateway.LyftRideCostEstimateResourceMock != null &&
                    TestMediatorForLyftGateway.ResponseString != null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Not Null.");
                }

                httpResponseMessage = new HttpResponseMessage(TestMediatorForLyftGateway.HttpStatusCodeForLyftRideCostEstimate)
                {
                    ReasonPhrase = TestMediatorForLyftGateway.ReasonPhrase,
                    Content = TestMediatorForLyftGateway.LyftRideCostEstimateResourceMock == null
                        ? new StringContent(TestMediatorForLyftGateway.ResponseString)
                        : new StringContent(JsonConvert.SerializeObject(TestMediatorForLyftGateway.LyftRideCostEstimateResourceMock), Encoding.UTF8, "application/json")
                };

                return await Task.FromResult(httpResponseMessage);
            }

            if (request.RequestUri.AbsolutePath.Contains("eta"))
            {
                if (TestMediatorForLyftGateway.LyftRideEtaEstimateResourceMock == null &&
                    TestMediatorForLyftGateway.ResponseString == null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Null.");
                }

                if (TestMediatorForLyftGateway.LyftRideEtaEstimateResourceMock != null &&
                    TestMediatorForLyftGateway.ResponseString != null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Not Null.");
                }

                httpResponseMessage = new HttpResponseMessage(TestMediatorForLyftGateway.HttpStatusCodeForLyftRideEtaEstimate)
                {
                    ReasonPhrase = TestMediatorForLyftGateway.ReasonPhrase,
                    Content = TestMediatorForLyftGateway.LyftRideEtaEstimateResourceMock == null
                        ? new StringContent(TestMediatorForLyftGateway.ResponseString)
                        : new StringContent(JsonConvert.SerializeObject(TestMediatorForLyftGateway.LyftRideEtaEstimateResourceMock), Encoding.UTF8, "application/json")
                };

                return await Task.FromResult(httpResponseMessage);
            }

            throw new NotImplementedException($"The Absolute Path: {request.RequestUri.AbsolutePath}, has no logic defined yet.");
        }

        private async Task<HttpResponseMessage> GetMockedUberServiceResponse(HttpRequestMessage request)
        {
            HttpResponseMessage httpResponseMessage = null;

            if (request.RequestUri.AbsolutePath.Contains("products"))
            {
                if (TestMediatorForUberGateway.UberRideTypeResourceMock == null &&
                    TestMediatorForUberGateway.ResponseString == null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Null.");
                }

                if (TestMediatorForUberGateway.UberRideTypeResourceMock != null &&
                    TestMediatorForUberGateway.ResponseString != null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Not Null.");
                }

                httpResponseMessage = new HttpResponseMessage(TestMediatorForUberGateway.HttpStatusCodeForUberRideTypes)
                {
                    ReasonPhrase = TestMediatorForUberGateway.ReasonPhrase,
                    Content = TestMediatorForUberGateway.UberRideTypeResourceMock == null
                        ? new StringContent(TestMediatorForUberGateway.ResponseString)
                        : new StringContent(JsonConvert.SerializeObject(TestMediatorForUberGateway.UberRideTypeResourceMock), Encoding.UTF8, "application/json")
                };

                return await Task.FromResult(httpResponseMessage);
            }

            if (request.RequestUri.AbsolutePath.Contains("price"))
            {
                if (TestMediatorForUberGateway.UberRideCostEstimateResourceMock == null &&
                    TestMediatorForUberGateway.ResponseString == null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Null.");
                }

                if (TestMediatorForUberGateway.UberRideCostEstimateResourceMock != null &&
                    TestMediatorForUberGateway.ResponseString != null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Not Null.");
                }

                httpResponseMessage = new HttpResponseMessage(TestMediatorForUberGateway.HttpStatusCodeForUberRideCostEstimate)
                {
                    ReasonPhrase = TestMediatorForUberGateway.ReasonPhrase,
                    Content = TestMediatorForUberGateway.UberRideCostEstimateResourceMock == null
                        ? new StringContent(TestMediatorForUberGateway.ResponseString)
                        : new StringContent(JsonConvert.SerializeObject(TestMediatorForUberGateway.UberRideCostEstimateResourceMock), Encoding.UTF8, "application/json")
                };

                return await Task.FromResult(httpResponseMessage);
            }

            if (request.RequestUri.AbsolutePath.Contains("time"))
            {
                if (TestMediatorForUberGateway.UberRideEtaEstimateResourceMock == null &&
                    TestMediatorForUberGateway.ResponseString == null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Null.");
                }

                if (TestMediatorForUberGateway.UberRideEtaEstimateResourceMock != null &&
                    TestMediatorForUberGateway.ResponseString != null)
                {
                    throw new Exception(ExceptionMessage + " Found both to be Not Null.");
                }

                httpResponseMessage = new HttpResponseMessage(TestMediatorForUberGateway.HttpStatusCodeForUberRideEtaEstimate)
                {
                    ReasonPhrase = TestMediatorForUberGateway.ReasonPhrase,
                    Content = TestMediatorForUberGateway.UberRideEtaEstimateResourceMock == null
                        ? new StringContent(TestMediatorForUberGateway.ResponseString)
                        : new StringContent(JsonConvert.SerializeObject(TestMediatorForUberGateway.UberRideEtaEstimateResourceMock), Encoding.UTF8, "application/json")
                };
                
                return await Task.FromResult(httpResponseMessage);
            }

            throw new NotImplementedException($"The Absolute Path: {request.RequestUri.AbsolutePath}, has no logic defined yet.");
        }
    }
}
