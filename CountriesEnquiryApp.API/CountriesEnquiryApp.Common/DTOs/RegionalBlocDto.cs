using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesEnquiryApp.Common.DTOs
{
    public class RegionalBlocDto
    {
        /// <summary>
        /// Gets or sets the name of the regionalBloc
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the code of the regional bloc
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets countries of the regional bloc
        /// </summary>
        public string[] Countries { get; set; }
    }
}
