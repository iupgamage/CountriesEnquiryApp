using CountriesEnquiryApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesEnquiryApp.DAL.Interfaces
{
    public interface IEnquiriesDataService
    {
        Task<List<Country>> GetCountriesByNameAsync(string name);

        Task<List<Country>> GetCountriesByRegionAsync(string regionalBlocCode);   
    }
}
