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
        [DisableCors]
        [HttpPost]
        public async Task<ResponseModel> PostData([FromBody]CompanyDto companyDto)
        {
            var json = JsonConvert.SerializeObject(companyDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://staging.miraclecbdproducts.com/api/company/";
            var rs = new ResponseModel()
            {
                Status = 200,
                Message = "Company account was created."
            };
            try
            {
                using (var db = new MiraclesContext())
                {
                    using (var client = new HttpClient())
                    {

                        HttpResponseMessage response = await client.PostAsync(url, data);
                        if (!response.IsSuccessStatusCode)
                        {
                            rs.Status = 400;
                            rs.Data = "";
                            rs.Message = "";
                            return rs;
                        }
                        else
                        {
                            db.TblMcompany.Add(new TblMcompany
                            {
                                ContactPerson = companyDto.contact_person.Trim(),
                                Name = companyDto.name.Trim(),
                                PhoneNumber = companyDto.phone_number.Trim(),
                                EmailAddress = companyDto.email_address.Trim(),
                                Username = companyDto.username.Trim(),
                                Password = companyDto.password.Trim(),
                                CurrentPassword = companyDto.password.Trim(),
                                ShopUrl = companyDto.shop_url.Trim()
                            });
                            db.SaveChanges();
                        }
                     
                    }
                }
            }
            catch (Exception ex)
            {
                rs.Status = 500;
                rs.Message = "Error" + ex.Message;
            }
            return rs;
        }
        
        //Scaffold-DbContext "Server=sql5045.site4now.net;Integrated Security=False;Database=DB_9A9CCA_shopify;
        //User ID=DB_9A9CCA_shopify_admin;Password=Vbn*34295;
        //MultipleActiveResultSets=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context MiraclesContext -Force
        [DisableCors]
        [HttpPatch("{username}")]
        public async Task<ResponseModel> UpdateCompany(string username, [FromBody]CompanyDto _companyDto)
        {

            var json = JsonConvert.SerializeObject(_companyDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://staging.miraclecbdproducts.com/api/company/";

            var rs = new ResponseModel()
            {
                Status = 200,
                Message = "Company account was updated."
            };
            try
            {
                using (var db = new MiraclesContext())
                {
                    using (var client = new HttpClient())
                    {

                        HttpResponseMessage response = await client.PatchAsync(url, data);
                        if (!response.IsSuccessStatusCode)
                        {
                            rs.Status = 400;
                            rs.Data = "";
                            rs.Message = "";
                            return rs;
                        }
                        else
                        {
                            var item = db.TblMcompany.Where(o => o.Username == username).FirstOrDefault();
                            if (item != null)
                            {
                                if (_companyDto.contact_person != null)
                                    item.ContactPerson = _companyDto.contact_person.Trim();
                                if (_companyDto.name != null)
                                    item.Name = _companyDto.name.Trim();
                                if (_companyDto.phone_number != null)
                                    item.PhoneNumber = _companyDto.phone_number.Trim();
                                if (_companyDto.email_address != null)
                                    item.EmailAddress = _companyDto.email_address.Trim();
                                if (_companyDto.confirm_new_password != null)
                                {
                                    item.Password = _companyDto.confirm_new_password.Trim();
                                    item.CurrentPassword = _companyDto.confirm_new_password.Trim();
                                }
                                if (_companyDto.shop_url != null)
                                    item.ShopUrl = _companyDto.shop_url.Trim();
                            }
                            db.SaveChanges();
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {
                rs.Status = 500;
                rs.Message = "Error" + ex.Message;
            }
            return rs;
        }
       
        [DisableCors]
        [HttpGet("{shopurl}")]

        public TblMcompany GetCompanyProfile(string shopurl)
        {
            
            using (var db = new MiraclesContext())
            {
                var rs = db.TblMcompany.Select(o => o.ShopUrl).ToList();
                var tb = rs[2];
                int i = 0;
                while (i < rs.Count)
                {
                    if (checkstring(rs[i], shopurl))
                    {
                        var result = db.TblMcompany.Where(o => o.ShopUrl == rs[i]).FirstOrDefault();
                        return result;
                    }
                    else
                        i++;
                }
                    return null;
                
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
       public bool checkstring(string nameData, string nameUrl)
        {
            if (nameData != null)
            {
                nameData = nameData.Trim();
                nameData = nameData.Replace(" ", "");
            }
            else
            {
                return false;
            }
            if (nameData == nameUrl)
                return true;
            return false;
        }
        

        public static bool IsPhoneNumber(string number)
        {
            string pattern = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";
            return Regex.IsMatch(number, pattern);
        }
    }

   
}
