using CountriesEnquiryApp.Common.Enums;
using CountriesEnquiryApp.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using UAParser;

namespace CountriesEnquiryApp.Common.Services
{
    public class ContextAccessor : IContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string BrowserName
        {
            get
            {
                var userAgent = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.UserAgent].ToString();
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(userAgent);
                return c.UA.Family;
            }
        }

        public string TimeStamp
        {
            get
            {
                return _httpContextAccessor.HttpContext.Items[RequestContext.RequestMadeAt].ToString();
            }
        }
    }
}
