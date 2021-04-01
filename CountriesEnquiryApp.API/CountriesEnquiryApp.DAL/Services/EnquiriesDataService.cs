using CountriesEnquiryApp.Common.Constants;
using CountriesEnquiryApp.Common.Helpers;
using CountriesEnquiryApp.Common.Models;
using CountriesEnquiryApp.DAL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CountriesEnquiryApp.DAL.Services
{
    public class EnquiriesDataService : IEnquiriesDataService
    {
        private readonly IHttpClientFactory _clientFactory;

        public EnquiriesDataService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Country>> GetCountriesByNameAsync(string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            $"{Constants.RestCountriesBaseURL}/name/{name}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<Country>>(responseString);
                return countries;
            }

            throw new CountryNotFoundException("name", name);
        }

        public async Task<List<Country>> GetCountriesByRegionAsync(string regionalBlocCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            $"{Constants.RestCountriesBaseURL}/regionalbloc/{regionalBlocCode}?fields=translations");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var countriesByRegion = JsonConvert.DeserializeObject<List<Country>>(responseString); 
                return countriesByRegion;
            }

            throw new CountryNotFoundException("regionalBlocCode", regionalBlocCode);
        }
    }
}
