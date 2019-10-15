﻿using Microsoft.AspNetCore.Authorization;
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
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyModel;
using System.Text;
using Newtonsoft.Json;
using ServiceStack.Text;
using Microsoft.AspNetCore.Http;
using System.Net;

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

        public async Task<string> PostURI([FromBody]CompanyDto _companyDto)
        {
            Uri u = new Uri("http://staging.miraclecbdproducts.com/api/company");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    

                        HttpContent content = new StringContent(
                        JsonConvert.SerializeObject(_companyDto),
                        Encoding.UTF8,
                        "application/json"
                    );
                    HttpResponseMessage response = await client.PostAsync(u, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        return string.Empty;
                    }
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }
      
        public async Task<String> UriPost([FromBody]string s)
        {
            string Login_URL1 = "http://staging.miraclecbdproducts.com/api/company";
            string Login_Data1 = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Pay_Load1 = new StringContent(Login_Data1, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Login_URL1, Pay_Load1);

                return await response.Content.ReadAsStringAsync();
            }
        }


        //[DisableCors]
        //[HttpPatch("{id}")]
        //public async Task<ResponseModel> UpdateCompany(int id, [FromBody]CompanyDto _companyDto)
        //{
        //    var rs = new ResponseModel()
        //    {
        //        Status = 200,
        //        Message = "Company account was updated."
        //    };
        //    try
        //    {
        //        using (var db = new MiraclesContext())
        //        {
                  
        //            var item = db.TblCompany.Where(o => o.Id == id).FirstOrDefault();
        //                if (item !=null)
        //            {
        //                if (_companyDto.ContactPerson !=null)
        //                item.Contact_Person = _companyDto.ContactPerson;
        //                if (_companyDto.Name != null)
        //                item.Name = _companyDto.Name;
        //                if (_companyDto.PhoneNumber !=null)
        //                item.Phone_Number = _companyDto.PhoneNumber;
        //                if (_companyDto.EmailAddress != null)
        //                item.Email_Address = _companyDto.EmailAddress;
        //                if (_companyDto.Password != null)
        //                item.Password = _companyDto.Password;
        //            }
                  
        //            else
        //            {
        //                rs.Status = 400;
        //                rs.Message = "Member is not found";
        //                return rs;
        //            }
        //            db.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rs.Status = 500;
        //        rs.Message = "error: " + ex.Message;
        //    }

        //    return rs;
        //}
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
      
        public static bool IsPhoneNumber(string number)
        {
            string pattern = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";
            return Regex.IsMatch(number, pattern);
        }
    }

   
}