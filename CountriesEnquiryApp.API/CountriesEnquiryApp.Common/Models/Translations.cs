using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesEnquiryApp.Common.Models
{
    public class Translations
    {
        [JsonProperty("nl")]
        public string NL { get; set; }
    }
}
