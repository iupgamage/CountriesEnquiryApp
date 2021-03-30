using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesEnquiryApp.Common.Models
{
    public class Country
    {
        /// <summary>
        /// Gets or sets the Code
        /// </summary>
        [JsonProperty("alpha2Code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Translations
        /// </summary>
        [JsonProperty("translations")]
        public Translations Translations { get; set; }

        /// <summary>
        /// Gets or sets RegionalBlocs
        /// </summary>
        public List<RegionalBloc> RegionalBlocs { get; set; }
    }
}
