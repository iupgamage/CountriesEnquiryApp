using CountriesEnquiryApp.Common.DTOs;
using CountriesEnquiryApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesEnquiryApp.BAL.Interfaces
{
    public interface IEnquiriesBusinessService
    {
        Task<List<CountryDto>> EnquireCountries(string name); 
    }
}
