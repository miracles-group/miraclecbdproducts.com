using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiraclecBDProducts.Models;
using ShopifySharp;

namespace MiraclecBDProducts.Controllers
{
        [AllowAnonymous]
        [EnableCors("AllowAllHeaders")]
        [Route("api/setting")]
        [ApiController]
       public class SettingController : ControllerBase
    {
        private readonly IConfiguration _config;
        private MiraclesContext _miraclesContext;

        public SettingController (IConfiguration config)
        {
            _config = config;
        }

        [DisableCors]
        [HttpPost]
        public ResponseModel Post([FromBody]Setting _setting)
        {
            var rs = new ResponseModel()
            {
                Status = 200,
                Message = string.Empty
            };
            try
            {
                var db = new MiraclesContext();
                {
                    var setting = db.Setting.FirstOrDefault();
                    if (setting != null)
                    {
                        setting.AutoSyncProduct = _setting.AutoSyncProduct;
                    }
                    else
                    {
                        setting = new Setting()
                        {
                            AutoSyncProduct = _setting.AutoSyncProduct
                        };
                    }
                    db.Update(setting);
                    db.SaveChanges();
                    rs.Data = setting;
                }
            }
            catch (Exception ex)
            {
                rs.Status = 500;
                rs.Message = ex.Message;
            }
            return rs;
        }
        [DisableCors]
        [HttpGet]
        public List<Setting> Get()
        {
            var db = new MiraclesContext();
            var rs = db.Setting.Where( o => o.AutoSyncProduct != null).ToList();
            return rs;

        }
        
           
                
    }

}
