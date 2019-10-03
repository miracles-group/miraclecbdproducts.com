using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MiraclecBDProducts.Models;
using Newtonsoft.Json;
using ShopifySharp;

namespace MiraclecBDProducts.Controllers
{
    [EnableCors("AllowAllHeaders")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public OrdersController(IConfiguration config, ILogger<OrdersController> logger)
        {
            _config = config;
            _logger = logger;
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
        public async Task<ResponseModel> Post([FromBody]Order shopifyorder)
        {
            var rs = new ResponseModel()
            {
                Status = 200,
                Message = string.Empty
            };
            try
            {
                _logger.LogCritical("SHOPIFYHOOK");
                _logger.LogCritical(JsonConvert.SerializeObject(shopifyorder));
                using (var httpClient = new HttpClient())
                {
                    var url = _config.GetValue<string>("miraclecbdproducts:Url");
                    var username = _config.GetValue<string>("miraclecbdproducts:username");
                    var password = _config.GetValue<string>("miraclecbdproducts:password");
                    var client = new HttpClient
                    {
                        BaseAddress = new Uri(url)
                    };
                    var order = new OrderModel
                    {
                        username = username,
                        password = password
                    };
                    if(shopifyorder.BillingAddress != null)
                    {
                        order.billing_address_first_name = shopifyorder.BillingAddress.FirstName;
                        order.billing_address_last_name = shopifyorder.BillingAddress.LastName;
                        order.billing_address_address1 = shopifyorder.BillingAddress.Address1;
                        order.billing_address_address2 = shopifyorder.BillingAddress.Address2;
                        order.billing_address_city = shopifyorder.BillingAddress.City;
                        order.billing_address_zip_code = shopifyorder.BillingAddress.Zip;
                        order.billing_address_state = shopifyorder.BillingAddress.Province;
                        order.billing_address_email = shopifyorder.Email;
                        order.billing_address_phone = shopifyorder.Phone;
                    }
                    if (shopifyorder.ShippingAddress != null)
                    {
                        order.shipping_address_first_name = shopifyorder.ShippingAddress.FirstName;
                        order.shipping_address_last_name = shopifyorder.ShippingAddress.LastName;
                        order.shipping_address_address1 = shopifyorder.ShippingAddress.Address1;
                        order.shipping_address_address2 = shopifyorder.ShippingAddress.Address2;
                        order.shipping_address_city = shopifyorder.ShippingAddress.City;
                        order.shipping_address_zip_code = shopifyorder.ShippingAddress.Zip;
                        order.shipping_address_state = shopifyorder.ShippingAddress.Province;
                        order.shipping_address_email = shopifyorder.Email;
                        order.shipping_address_phone = shopifyorder.Phone;
                    }
                    order.products = new List<ProductModel>();
                    foreach (var product in shopifyorder.LineItems)
                    {
                        order.products.Add(new ProductModel
                        {
                            id = product.Id,
                            quantity = product.Quantity
                        });
                    }
                    var response = await client.PostAsJsonAsync(url, order);
                    rs.Data = order;
                }
            }
            catch (Exception ex)
            {
                rs.Status = 500;
                rs.Message = ex.Message;
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
