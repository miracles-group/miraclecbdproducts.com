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
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyModel;
using System.Text;
using Newtonsoft.Json;
using ServiceStack.Text;
using Microsoft.AspNetCore.Http;
using System.Net;
using MiraclecBDProducts.Helpers;

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
        public async Task<string> PostURL([FromBody]CompanyDto companyDto)
        {
            var json = JsonConvert.SerializeObject(companyDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://staging.miraclecbdproducts.com/api/company/";
            try
            {
                using (var client = new HttpClient())
                {

                    HttpResponseMessage response = await client.PostAsync(url, data);
                    if (!response.IsSuccessStatusCode)
                    {
                        return string.Empty;
                    }
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }
        
        //public async Task<string> UriPost([FromBody]CompanyDto companyDto)
        //{
            
        //    using (var client = new HttpClient())
        //    {
        //        var request = new
        //        {
        //            Url = "http://staging.miraclecbdproducts.com/api/company",
        //            Body = new
        //            {
        //                contact_person = companyDto.Contact_Person,
        //                name = companyDto.Name,
        //                phone_number = companyDto.Phone_Number,
        //                email_address = companyDto.Email_Address,
        //                username = companyDto.Username,
        //                password = companyDto.Password

        //            }
        //        };
        //        var response = await client.PostAsJsonAsync(request.Url, ContentHelper.GetStringContent(request.Body));
        //        var value = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        //        return response.EnsureSuccessStatusCode().ToString();
        //    }
        //}


        [DisableCors]
        [HttpPatch]
        public async Task<string> UpdateCompany([FromBody]CompanyDto _companyDto)
        {

            var json = JsonConvert.SerializeObject(_companyDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://staging.miraclecbdproducts.com/api/company/";

           
            try
            {
                using (var client = new HttpClient())
                {
                  
                  HttpResponseMessage response = await client.PatchAsync(url, data);
                    if (!response.IsSuccessStatusCode)
                    {
                        return string.Empty;
                    }
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }
       
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
