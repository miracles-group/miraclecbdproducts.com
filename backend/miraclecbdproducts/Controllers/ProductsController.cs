using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiraclecBDProducts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopifySharp;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using MiraclecBDProducts.Services;
using MiraclecBDProducts.Dto;

namespace MiraclecBDProducts.Controllers
{
    [EnableCors("AllowAllHeaders")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProductsController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [DisableCors]
        [HttpPost]
        public async Task<ResponseModel> Post([FromBody] Product product)
        {
            var myShopifyUrl = _config.GetValue<string>("Shopify:Url");
            var privateAppPassword = _config.GetValue<string>("Shopify:PrivateAppPassword");
            return await ProductServices.AddProduct(product, myShopifyUrl, privateAppPassword);
        }
        List<MiraclesProduct> miraclesProducts { get; set; }
       
        public async Task<bool> GetMiraclesProductAsync()
        {
            String url = "https://miraclecbdproducts.com/api/product/?username=phamminh1309";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string datastring = await response.Content.ReadAsStringAsync();
            JObject datajson = JObject.Parse(datastring);
            MiraclesResponse miraclesResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MiraclesResponse>(datastring);
            miraclesProducts = miraclesResponse.Results;
            var myShopifyUrl = _config.GetValue<string>("Shopify:Url");
            var privateAppPassword = _config.GetValue<string>("Shopify:PrivateAppPassword");
            foreach (var product in miraclesProducts)
            {
                var shopifyProduct = new Product {
                    Id = product.Id,
                    Title = product.Name,
                    BodyHtml = product.Description,
                    CreatedAt= DateTime.UtcNow.Date,
                    
                };
                var rs = await ProductServices.AddProduct(shopifyProduct, myShopifyUrl, privateAppPassword);
            }
            return true;
        }
        [DisableCors]
        [HttpGet("autosync")]
        public async Task<ResponseModel> AutoAsyncMiraclesProduct()
        {
            using (var db = new MiraclesContext())
            {
                try

                {
                    var check = db.Setting.Select(o => o.AutoSyncProduct).FirstOrDefault();
                    if (check != true)
                    {
                        return null;
                    }

                    await GetMiraclesProductAsync();
 
                    db.TblAuditLog.Add(new TblAuditLog
                    {
                        CreatedAt = DateTime.UtcNow,
                        Message = "Auto sync is completed."
                    });
                    db.SaveChanges();
                    return new ResponseModel
                    {
                        Status = 200,
                        Message = "Auto sync is completed."
                    };

                }

                catch (Exception ex)
                {
                    var message = "Auto sync is failed. Message: " + ex.Message + ". Trace: " + ex.StackTrace;
                    db.TblAuditLog.Add(new TblAuditLog
                    {
                        CreatedAt = DateTime.UtcNow,
                        Message = message
                    });
                    db.SaveChanges();
                    return new ResponseModel
                    {
                        Status = 200,
                        Message = message
                    };
                }
            }
        }
        [HttpPatch("{id}")]
        public async Task<ResponseModel> UpdateProduct(int id, [FromBody]ProductDto productDto)
        {
            var rs = new ResponseModel()
            {
                Status = 200,
                Message = "Product was updated."
            };
            try
            {
                using (var db = new MiraclesContext())
                {
                    var myShopifyUrl = _config.GetValue<string>("Shopify:Url");
                    var privateAppPassword = _config.GetValue<string>("Shopify:PrivateAppPassword");
                    var service = new ProductService(myShopifyUrl, privateAppPassword);
                    var item = db.MappingOrder.FirstOrDefault(o => o.MiraclesId == id);

                    if (item == null)
                    {
                        rs.Status = 400;
                        rs.Message = "Dont have Product you want to update.";
                        return rs;
                    }
                    else
                    {
                       
                            
                    }


                }
            }
            catch (Exception ex)
            {
                rs.Status = 500;
                rs.Message = "error: " + ex.Message;
            }

            return rs;

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
