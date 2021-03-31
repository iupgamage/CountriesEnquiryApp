using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesEnquiryApp.Common.Interfaces
{
    public interface IContextAccessor
    {
        string BrowserName { get; }

        string TimeStamp { get; } 
    }
}
