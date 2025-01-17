﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MiraclecBDProducts.Models;
using MiraclecBDProducts.Services;

namespace MiraclecBDProducts.Controllers
{

    [Route("api/Company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _ICompanyService;
        //private IMapper _mapper;
        //private readonly AppSettings _appSettings;
        public CompanyController(
        ICompanyService companyService)
        {
            _ICompanyService = companyService;
            //_mapper = mapper;
            //_appSettings = appSettings.Value;
        }



        //[Authorize]
        [DisableCors]
        [HttpGet("get-company")]
        public IActionResult getCompany()
        {
            return Ok(_ICompanyService.getCompany());
        }



        [Authorize]
        [DisableCors]
        [HttpPost("add-company")]
        public IActionResult addCompany(CustomerModel tblcustomer)
        {
            var x = _ICompanyService.addCompany(tblcustomer);
            return Ok(x);
        }


        [Authorize]
        [DisableCors]
        [HttpPost("delete-arr-company")]
        public IActionResult DeleteArrCompany(List<string> deleteIds)
        {
            return Ok(_ICompanyService.DeleteArrCompany(deleteIds));
        }

        [Authorize]
        [DisableCors]
        [HttpPut("edit-company")]
        public IActionResult EditCompany(CustomerModel customer)
        {
            return Ok(_ICompanyService.EditCompany(customer));
        }

    }
}