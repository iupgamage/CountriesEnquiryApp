using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesEnquiryApp.Common.Models
{
    public class CountryName
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
