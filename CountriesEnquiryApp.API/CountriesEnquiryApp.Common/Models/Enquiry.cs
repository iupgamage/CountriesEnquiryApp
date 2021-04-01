using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountriesEnquiryApp.Common.Models
{
    public class Enquiry
    {
        [Required]
        public string Name { get; set; }
    }
}
