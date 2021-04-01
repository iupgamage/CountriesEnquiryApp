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
using System.Linq;

namespace CountriesEnquiryApp.Tests
{
    public class EnquiriesDataService_Tests
    {
        IHttpClientFactory httpClientFactory;

        private void SetupMocks()
        {
            #region Create moq objects

            var httpClientFactoryMoq = new Mock<IHttpClientFactory>();

            #endregion

            #region Setup the returnables

            var countryListByName = new List<Country> {
                new Country {
                    Code="LK",
                    RegionalBlocs=new List<RegionalBloc> {
                       new RegionalBloc{
                           Code="SAARC",
                           Name="South Asian Association for Regional Cooperation"
                       }
                    },
                    Translations=new Translations {
                        NL="Sri Lanka"
                    }
                }
            };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    //Content = new StringContent("{'name':thecodebuzz,'city':'USA'}"),
                    Content = new StringContent(JsonConvert.SerializeObject(countryListByName)),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMoq.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            #endregion

            #region Assign to Object

            httpClientFactory = httpClientFactoryMoq.Object;

            #endregion
        }

        [Fact]
        public async Task GetCountriesByNameAsync_ReturnsListOfCountries() 
        {
            //Arrange the resources
            SetupMocks();
            var service = new EnquiriesDataService(httpClientFactory);
            string countryName = "sri";

            //Act on the functionality
            List<Country> countries = await service.GetCountriesByNameAsync(countryName);

            //Assert the result against the expected
            Assert.True(countries.Count() > 0);
        }
    }
}
