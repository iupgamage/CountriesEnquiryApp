using AutoMapper;
using CountriesEnquiryApp.BAL.Services;
using CountriesEnquiryApp.Common.Interfaces;
using CountriesEnquiryApp.Common.Models;
using CountriesEnquiryApp.DAL.Interfaces;
using CountriesEnquiryApp.DAL.Services;
using CountriesEnquiryApp.Messaging.Interfaces;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using CountriesEnquiryApp.Common.Mapper;
using CountriesEnquiryApp.Common.DTOs;

namespace CountriesEnquiryApp.Tests
{
    public class EnquiriesBusinessService_Tests
    {
        IEnquiriesDataService enquiriesDataService;
        IContextAccessor contextAccessor;
        IServiceBusMessageSender serviceBusMessageSender;
        IMapper mapper; 
        string browserName = "My browser"; 
        string timestamp = "My timestamp"; 

        private void SetupMocks()
        {
            //Create moq objects
            var enquiriesDataServiceMoq = new Mock<IEnquiriesDataService>();

            var contextAccessorMoq = new Mock<IContextAccessor>();

            var serviceBusMessageSenderMoq = new Mock<IServiceBusMessageSender>();

            //Setup the returnables

            var countryList = new List<Country> {
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

            enquiriesDataServiceMoq
                .Setup(x => x.GetCountriesByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(countryList));

            var countryNameList = new List<CountryName> {
                new CountryName{
                    Name="Afghanistan"
                },
                new CountryName{
                    Name="Bangladesh"
                },
                new CountryName{
                    Name="Bhutan"
                },
                new CountryName{
                    Name="India"
                },
                new CountryName{
                    Name="Maldives"
                },
                new CountryName{
                    Name="Nepal"
                },
                new CountryName{
                    Name="Pakistan"
                },
                new CountryName{
                    Name="Sri Lanka"
                }
            };

            enquiriesDataServiceMoq
                .Setup(x => x.GetCountriesByRegionAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(countryNameList));

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping()); 
            });

            //Assign to Object
            enquiriesDataService = enquiriesDataServiceMoq.Object;

            contextAccessor = contextAccessorMoq.Object;

            serviceBusMessageSender = serviceBusMessageSenderMoq.Object;

            mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task GetCountriesByName_ReturnsListOfCountries111()  
        {
            //Arrange the resources
            SetupMocks();
            var a = new EnquiriesBusinessService(enquiriesDataService, contextAccessor, serviceBusMessageSender, mapper);
            string countryName = "au";

            //Act on the functionality
            var response = await a.EnquireCountries(countryName);

            //Assert the result against the expected
            Assert.True(response.CountriesEnq.FirstOrDefault().Code == "LK"); 
        }
    }
}
