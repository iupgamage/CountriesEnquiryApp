using CountriesEnquiryApp.BAL.Interfaces;
using CountriesEnquiryApp.Common.Models;
using CountriesEnquiryApp.DAL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CountriesEnquiryApp.Common.Services;
using AutoMapper;
using CountriesEnquiryApp.Messaging.Services;
using CountriesEnquiryApp.Common.DTOs;
using CountriesEnquiryApp.DAL.Interfaces;
using CountriesEnquiryApp.Common.Interfaces;
using CountriesEnquiryApp.Messaging.Interfaces;

namespace CountriesEnquiryApp.BAL.Services
{
    public class EnquiriesBusinessService : IEnquiriesBusinessService
    {
        private readonly IEnquiriesDataService _enquiriesDataService;
        private readonly IContextAccessor _contextAccessor;
        private readonly IServiceBusMessageSender _serviceBusMessageSender;
        private readonly IMapper _mapper;
        private readonly string _browserName;
        private readonly string _timestamp;

        public EnquiriesBusinessService(IEnquiriesDataService enquiriesDataService, IContextAccessor contextAccessor, IServiceBusMessageSender serviceBusMessageSender, IMapper mapper)
        {
            _enquiriesDataService = enquiriesDataService;
            _contextAccessor = contextAccessor;
            _serviceBusMessageSender = serviceBusMessageSender;
            _mapper = mapper;
            _browserName = _contextAccessor.BrowserName;
            _timestamp = _contextAccessor.TimeStamp;
        }

        public async Task<Response> EnquireCountries(string name)
        {
            var response = new Response();

            var countryList = await _enquiriesDataService.GetCountriesByNameAsync(name);

            var countryDtoList = _mapper.Map<List<CountryDto>>(countryList);

            foreach (var countryDto in countryDtoList)
            {
                foreach (var region in countryDto.RegionalBlocs)
                {
                    var countryNameList = await _enquiriesDataService.GetCountriesByRegionAsync(region.Code);
                    var countryNameArray = countryNameList.Select(c => c.Name).ToArray();
                    region.Countries = countryNameArray;
                }

                countryDto.BrowserName = _browserName;
                countryDto.Timestamp = _timestamp;

                await _serviceBusMessageSender.SendMessageAsync(countryDto);
            }

            response.CountriesEnq = countryDtoList;

            return response;
        }
    }
}
