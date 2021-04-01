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

        private void SetupMocks()
        {
            #region Create moq objects

            var enquiriesDataServiceMoq = new Mock<IEnquiriesDataService>();

            var contextAccessorMoq = new Mock<IContextAccessor>();

            var serviceBusMessageSenderMoq = new Mock<IServiceBusMessageSender>();

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

            enquiriesDataServiceMoq
                .Setup(x => x.GetCountriesByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(countryListByName));

            var countryListByRegion = new List<Country> {
                new Country{
                    Translations=new Translations{ NL="Afghanistan"}
                },
                new Country{
                    Translations=new Translations{ NL="Bangladesh"}
                },
                new Country{
                    Translations=new Translations{ NL="Bhutan"}
                },
                new Country{
                    Translations=new Translations{ NL="India"}
                },
                new Country{
                    Translations=new Translations{ NL="Maldiven"}
                },
                new Country{
                    Translations=new Translations{ NL="Nepal"}
                },
                new Country{
                    Translations=new Translations{ NL="Pakistan"}
                },
                new Country{
                    Translations=new Translations{ NL="Sri Lanka"}
                }
            };

            enquiriesDataServiceMoq
                .Setup(x => x.GetCountriesByRegionAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(countryListByRegion));

            contextAccessorMoq
                .Setup(x => x.BrowserName).Returns("Chrome");

            contextAccessorMoq
                .Setup(x => x.TimeStamp).Returns("3/30/2021 10:31:51 PM");

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping()); 
            });

            #endregion

            #region Assign to Object

            enquiriesDataService = enquiriesDataServiceMoq.Object;

            contextAccessor = contextAccessorMoq.Object;

            serviceBusMessageSender = serviceBusMessageSenderMoq.Object;

            mapper = mockMapper.CreateMapper();

            #endregion
        }

        [Fact]
        public async Task EnquireCountries_ReturnsListOfCountries()   
        {
            //Arrange the resources
            SetupMocks();
            var service = new EnquiriesBusinessService(enquiriesDataService, contextAccessor, serviceBusMessageSender, mapper);
            string countryName = "sri";

            //Act on the functionality
            var response = await service.EnquireCountries(countryName);

            //Assert the result against the expected
            Assert.True(response.Count() > 0);
        }
    }
}
