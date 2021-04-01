using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesEnquiryApp.Common.Models
{
    public class RegionalBloc
    {
        /// <summary>
        /// Gets or sets the name of the regional bloc
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the code of the regional bloc
        /// </summary>
        [JsonProperty("acronym")]
        public string Code { get; set; }
    }
}
