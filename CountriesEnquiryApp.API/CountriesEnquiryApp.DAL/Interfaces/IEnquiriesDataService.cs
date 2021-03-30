using CountriesEnquiryApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesEnquiryApp.DAL.Interfaces
{
    interface IEnquiriesDataService
    {
        Task<List<Country>> GetCountriesByNameAsync(string name);

        Task<List<CountryName>> GetCountriesByRegionAsync(string regionalBlocCode);   
    }
}
