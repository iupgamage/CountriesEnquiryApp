using CountriesEnquiryApp.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CountriesEnquiryApp.Common.Models
{
    public class Response
    {
        public Response()
        {
            CountriesEnq = new List<CountryDto>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        //public EnquiryResponse EnquiryResponse { get; set; }

        public List<CountryDto> CountriesEnq { get; set; } 
    }
} 
