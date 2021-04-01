using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesEnquiryApp.BAL.Interfaces;
using CountriesEnquiryApp.BAL.Services;
using CountriesEnquiryApp.Common.Helpers;
using CountriesEnquiryApp.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountriesEnquiryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiriesController : ControllerBase
    {
        private readonly IEnquiriesBusinessService _enquiriesBusinessService;

        public EnquiriesController(IEnquiriesBusinessService enquiriesBusinessService)
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
            catch (CountryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}