using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiraclecBDProducts.Models;
using ShopifySharp;

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
            var rs = new ResponseModel()
            {
                Status = 200,
                Message = string.Empty
            };
            try
            {
                using (var db = new MiraclesContext())
                {
                    var myShopifyUrl = _config.GetValue<string>("Shopify:Url");
                    var privateAppPassword = _config.GetValue<string>("Shopify:PrivateAppPassword");
                    var service = new ProductService(myShopifyUrl, privateAppPassword);
                    if (!product.Id.HasValue)
                    {
                        rs.Status = 500;
                        rs.Message = "Product ID is required";
                        return rs;
                    }
                    if (string.IsNullOrEmpty(product.Title))
                    {
                        rs.Status = 500;
                        rs.Message = "Title is required";
                        return rs;
                    }
                    long miraclesID = product.Id.Value;
                    var item = db.MappingOrder.FirstOrDefault(o => o.MiraclesId == miraclesID);
                    var shopifyProduct = new Product();
                    if (item != null)
                    {
                        shopifyProduct = await service.UpdateAsync(item.ShopifyId, product);
                        
                    }
                    else
                    {
                        shopifyProduct = await service.CreateAsync(product);
                    }
                    
                    if (!shopifyProduct.Id.HasValue)
                    {
                        rs.Status = 500;
                        rs.Message = "Can not detect product ID from Shopify. Value: " + shopifyProduct.Id;
                        return rs;
                    }

                    var shopifyID = shopifyProduct.Id.Value;
                    if(item == null)
                    {
                        db.MappingOrder.Add(new MappingOrder()
                        {
                            MiraclesId = miraclesID,
                            ShopifyId = shopifyID
                        });
                    }
                    db.SaveChanges();
                }
                rs.Data = product;
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
