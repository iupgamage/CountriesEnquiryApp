using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesEnquiryApp.Common.Helpers
{
    public class CountryNotFoundException : Exception
    {
        public CountryNotFoundException(string field, string value)
        : base(string.Format("Country not found using field '{0}' with value '{1}.", field, value))
        {
        }
    }
}
