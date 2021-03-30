using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesEnquiryApp.Common.Interfaces
{
    interface IContextAccessor
    {
        string BrowserName { get; }

        string TimeStamp { get; } 
    }
}
