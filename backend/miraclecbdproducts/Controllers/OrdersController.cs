using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
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
            using (var httpClient = new HttpClient())
            {
                var url = _config.GetValue<string>("miraclecbdproducts:Url");
                var username = _config.GetValue<string>("miraclecbdproducts:username");
                var password = _config.GetValue<string>("miraclecbdproducts:password");
                var order = new OrderModel
                {
                    username = username,
                    password = password
                };
                try
                {
                    var client = new HttpClient
                    {
                        BaseAddress = new Uri(url)
                    }; 
                     var phone = !string.IsNullOrEmpty(shopifyorder.Phone) ? shopifyorder.Phone : "0123456789";
                    if (shopifyorder.BillingAddress != null)
                    {

                        var address2 = !string.IsNullOrEmpty(shopifyorder.BillingAddress.Address2) ? shopifyorder.BillingAddress.Address2 : shopifyorder.BillingAddress.Address1;
                        order.billing_address_first_name = shopifyorder.BillingAddress.FirstName;
                        order.billing_address_last_name = shopifyorder.BillingAddress.LastName;
                        order.billing_address_address1 = shopifyorder.BillingAddress.Address1;
                        order.billing_address_address2 = address2;
                        order.billing_address_city = shopifyorder.BillingAddress.City;
                        order.billing_address_zip_code = shopifyorder.BillingAddress.Zip;
                        order.billing_address_state = shopifyorder.BillingAddress.Province;
                        order.billing_address_email = shopifyorder.Email;
                        order.billing_address_phone = phone;
                    }
                    else
                    {
                        order.billing_address_first_name = "John";
                        order.billing_address_last_name = "Doe";
                        order.billing_address_address1 = "123 Billing Street";
                        order.billing_address_address2 = "123 Billing Street";
                        order.billing_address_city = "Billtown";
                        order.billing_address_zip_code = "K2P0B0";
                        order.billing_address_state = "Kentucky";
                        order.billing_address_email = "jon@doe.ca";
                        order.billing_address_phone = "0123456789";
                    }
                    if (shopifyorder.ShippingAddress != null)
                    {
                        var address2 = !string.IsNullOrEmpty(shopifyorder.ShippingAddress.Address2) ? shopifyorder.ShippingAddress.Address2 : shopifyorder.ShippingAddress.Address1;
                        order.shipping_address_first_name = shopifyorder.ShippingAddress.FirstName;
                        order.shipping_address_last_name = shopifyorder.ShippingAddress.LastName;
                        order.shipping_address_address1 = shopifyorder.ShippingAddress.Address1;
                        order.shipping_address_address2 = address2;
                        order.shipping_address_city = shopifyorder.ShippingAddress.City;
                        order.shipping_address_zip_code = shopifyorder.ShippingAddress.Zip;
                        order.shipping_address_state = shopifyorder.ShippingAddress.Province;
                        order.shipping_address_email = shopifyorder.Email;
                        order.shipping_address_phone = phone;
                    }
                    else
                    {
                        order.shipping_address_first_name = "John";
                        order.shipping_address_last_name = "Doe";
                        order.shipping_address_address1 = "123 shipping Street";
                        order.shipping_address_address2 = "123 shipping Street";
                        order.shipping_address_city = "Billtown";
                        order.shipping_address_zip_code = "K2P0B0";
                        order.shipping_address_state = "Kentucky";
                        order.shipping_address_email = "jon@doe.ca";
                        order.shipping_address_phone = "0123456789";
                    }
                    order.ip_address = "192.168.0.1";
                    order.products = new List<ProductModel>();
                    foreach (var product in shopifyorder.LineItems)
                    {
                        var miraclesID = GetMiraclesID(product.ProductId.Value);
                        if (miraclesID != 0)
                        {
                            order.products.Add(new ProductModel
                            {
                                id = miraclesID,
                                quantity = product.Quantity
                            });
                        }
                    }
                    if(order.products.Count() == 0)
                    {
                        rs.Status = 500;
                        rs.Message = "Order does not have any product";
                    }
                    else
                    {
                        var miraclesData = JsonConvert.SerializeObject(order);
                        var content = new StringContent(miraclesData, Encoding.UTF8, "application/json");
                        var response = client.PostAsync(url, content).Result;
                        rs.Status = (int)response.StatusCode;
                    }
                    rs.Data = order;

                }
                catch (Exception ex)
                {
                    rs.Status = 500;
                    rs.Message = ex.Message;
                }
                using(var db = new MiraclesContext())
                {
                    var inputData = JsonConvert.SerializeObject(shopifyorder);
                    var miraclesData = JsonConvert.SerializeObject(order); 
                    var message = "Status: " + rs.Status + "; Message: " + rs.Message;
                    if(rs.Status != 200)
                    {
                        message += "; Input Data: " + inputData;
                        message += "; Miracles Data: " + miraclesData;
                        message += "; Miracles URL: " + url;
                    }
                    db.TblAuditLog.Add(new TblAuditLog()
                    {
                        CreatedAt = DateTime.UtcNow,
                        Message = message
                    });
                    db.SaveChanges();
                }
            }
            return rs;
        }

        private long GetMiraclesID(long shopifyID)
        {
            using (var db = new MiraclesContext())
            {
                var product = db.MappingOrder.FirstOrDefault(o => o.ShopifyId == shopifyID);
                if (product != null)
                {
                    return product.MiraclesId;
                }
                return 0;
            }
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
