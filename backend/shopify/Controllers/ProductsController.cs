using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopifySharp;

namespace shopify.Controllers
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
            var rs = new ResponseModel() {
                Status = 200,
                Message = string.Empty
            };
            try
            {
                var myShopifyUrl = _config.GetValue<string>("Shopify:Url");
                var privateAppPassword = _config.GetValue<string>("Shopify:PrivateAppPassword");
                var service = new ProductService(myShopifyUrl, privateAppPassword);
                if(string.IsNullOrEmpty(product.Title))
                {
                    rs.Status = 500;
                    rs.Message = "Title is required";
                    return rs;
                }
                var createResult = await service.CreateAsync(product);
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
