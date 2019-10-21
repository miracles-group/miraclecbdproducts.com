using AutoMapper.Configuration;
using MiraclecBDProducts.Models;
using ShopifySharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiraclecBDProducts.Services
{
    public static class ProductServices
    {
        public static async Task<ResponseModel> AddProduct(Product product, string myShopifyUrl, string privateAppPassword)
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
                    if (item == null)
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
    }
}
