using CountriesEnquiryApp.Common.DTOs;
using CountriesEnquiryApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesEnquiryApp.Messaging.Interfaces
{
    public interface IServiceBusMessageSender
    {
        Task SendMessageAsync(CountryDto countryDto);
    }
}
