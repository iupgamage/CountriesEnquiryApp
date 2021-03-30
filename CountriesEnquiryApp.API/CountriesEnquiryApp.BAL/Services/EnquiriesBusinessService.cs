using CountriesEnquiryApp.BAL.Interfaces;
using CountriesEnquiryApp.Common.Models;
using CountriesEnquiryApp.DAL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using UAParser;
using CountriesEnquiryApp.Common.Enums;
using Microsoft.Net.Http.Headers;
using CountriesEnquiryApp.Common.Services;
using AutoMapper;
using CountriesEnquiryApp.Messaging.Services;
using CountriesEnquiryApp.Common.DTOs;

namespace CountriesEnquiryApp.BAL.Services
{
    public class EnquiriesBusinessService : IEnquiriesBusinessService
    {
        private readonly EnquiriesDataService _enquiriesDataService;
        private readonly ContextAccessor _contextAccessor;
        private readonly ServiceBusMessageSender _serviceBusMessageSender;
        private readonly IMapper _mapper;
        private readonly string _browserName;
        private readonly string _timestamp;

        public EnquiriesBusinessService(EnquiriesDataService enquiriesDataService, ContextAccessor contextAccessor, ServiceBusMessageSender serviceBusMessageSender, IMapper mapper)
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
