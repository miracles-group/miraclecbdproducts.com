using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MiraclecBDProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using AutoMapper;
using System.Text.RegularExpressions;
using MiraclecBDProducts.Dto;
using System.Threading.Tasks;

namespace MiraclecBDProducts.Controllers
{
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]
    [Route("api/company")]
    [ApiController]
    public class MCompanyController : ControllerBase
    {
     
        public MCompanyController()
        {

        }
        [DisableCors]
        [HttpPost]
        public async Task<ResponseModel> InsertCompany([FromBody]CompanyDto _companyDto)
        {
            var rs = new ResponseModel()
        {
           Status = 200,
            Message = "Company account was created."
        };
            try
            {
                using (var db = new MiraclesContext())
                {
                    if (IsValidEmail(_companyDto.EmailAddress) && IsPhoneNumber(_companyDto.PhoneNumber))
                        db.TblCompany.Add(new TblCompany
                        {
                            ContactPerson = _companyDto.ContactPerson,
                            Name = _companyDto.Name,
                            PhoneNumber = _companyDto.PhoneNumber,
                            EmailAddress = _companyDto.EmailAddress,
                            UserName = _companyDto.UserName,
                            Password = _companyDto.Password,
                        });
                    else
                    {
                        rs.Status = 400;
                        rs.Message = "Email address or phone number is invalid.";
                        return rs;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rs.Status = 500;
                rs.Message = "error: " + ex.Message;
            }
         
            return rs;

        }
        [DisableCors]
        [HttpPatch("{id}")]
        public async Task<ResponseModel> UpdateCompany(int id, [FromBody]CompanyDto _companyDto)
        {
            var rs = new ResponseModel()
            {
                Status = 200,
                Message = "Company account was updated."
            };
            try
            {
                using (var db = new MiraclesContext())
                {
                  
                    var item = db.TblCompany.Where(o => o.Id == id).FirstOrDefault();
                        if (item !=null)
                    {
                        if (_companyDto.ContactPerson !=null)
                        item.ContactPerson = _companyDto.ContactPerson;
                        if (_companyDto.Name != null)
                        item.Name = _companyDto.Name;
                        if (_companyDto.PhoneNumber !=null)
                        item.PhoneNumber = _companyDto.PhoneNumber;
                        if (_companyDto.EmailAddress != null)
                        item.EmailAddress = _companyDto.EmailAddress;
                        if (_companyDto.Password != null)
                        item.Password = _companyDto.Password;
                    }
                  
                    else
                    {
                        rs.Status = 400;
                        rs.Message = "Member is not found";
                        return rs;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rs.Status = 500;
                rs.Message = "error: " + ex.Message;
            }

            return rs;
        }
            public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(?<countryCode>[\+][1-9]{1}[0-9]{0,2}\s)?(?<areaCode>0?[1-9]\d{0,4})(?<number>\s[1-9][\d]{5,12})(?<extension>\sx\d{0,4})?$").Success;
        }
    }
   
}
