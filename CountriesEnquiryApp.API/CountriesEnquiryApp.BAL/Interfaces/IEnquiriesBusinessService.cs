using CountriesEnquiryApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesEnquiryApp.BAL.Interfaces
{
    public interface IEnquiriesBusinessService
    {
        Task<Response> EnquireCountries(string name);
    }
}
