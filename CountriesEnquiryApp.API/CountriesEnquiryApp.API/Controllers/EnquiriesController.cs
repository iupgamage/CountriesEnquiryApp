using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesEnquiryApp.BAL.Services;
using CountriesEnquiryApp.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountriesEnquiryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiriesController : ControllerBase
    {
        private readonly EnquiriesBusinessService _enquiriesBusinessService;

        public EnquiriesController(EnquiriesBusinessService enquiriesBusinessService)
        {
            _enquiriesBusinessService = enquiriesBusinessService;
        }

        [HttpPost("")]
        public async Task<IActionResult> PostEnquiry(Enquiry enquiry)
        {
            try
            {
                var response = await _enquiriesBusinessService.EnquireCountries(enquiry.Name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}