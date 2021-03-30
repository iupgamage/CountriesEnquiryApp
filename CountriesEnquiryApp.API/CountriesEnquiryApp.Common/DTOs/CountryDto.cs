using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesEnquiryApp.Common.DTOs
{
    public class CountryDto
    {
        /// <summary>
        /// Gets or sets the Dutch Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the BrowserName
        /// </summary>
        public string BrowserName { get; set; }

        /// <summary>
        /// Gets or sets the Timestamp
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// Gets or sets RegionalBlocs
        /// </summary>
        public List<RegionalBlocDto> RegionalBlocs { get; set; }
    }
}
