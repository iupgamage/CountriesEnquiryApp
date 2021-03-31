using CountriesEnquiryApp.Common.Models;
using CountriesEnquiryApp.DAL.Interfaces;
using CountriesEnquiryApp.DAL.Services;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CountriesEnquiryApp.Tests
{
    public class EnquiriesDataService_Tests
    {
        IHttpClientFactory httpClientFactory;

        private void SetupMocks()
        {
            //Create moq objects

            var httpClientFactoryMoq = new Mock<IHttpClientFactory>();

            //Setup the returnables

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    //Content = new StringContent("{'name':thecodebuzz,'city':'USA'}"),
                    Content = new StringContent(JsonConvert.SerializeObject(new List<Country>() { new Country() })),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMoq.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            //Assign to Object

            httpClientFactory = httpClientFactoryMoq.Object;
        }

        [Fact]
        public async Task GetCountriesByName_ReturnsListOfCountries()
        {
            //Arrange the resources
            SetupMocks();
            var a = new EnquiriesDataService(httpClientFactory);
            string countryName = "au";

            //Act on the functionality
            List<Country> countries = await a.GetCountriesByNameAsync(countryName);

            //Assert the result against the expected
            Assert.True(countries.Count == 1);
        }
    }
}
