using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiraclecBDProducts
{
    public class MiraclesResponse
    {
        public List<MiraclesProduct> Results { get; set; }
    }
    public class MiraclesProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Slug { get; set; }
        public string Catelogy { get; set; }
        public string Sub_catelogy { get; set; }
        public string Mg_of_cbd { get; set; }
        public string Ingredients { get; set; }
        public string Image_path { get; set; }

        public string Coa_path { get; set; }
    }
    public class ResponseModel
    {
        public int Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }

    public class OrderModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public List<ProductModel> products { get; set; }
        public string billing_address_first_name { get; set; }
        public string billing_address_last_name { get; set; }
        public string billing_address_address1 { get; set; }
        public string billing_address_address2 { get; set; }
        public string billing_address_city { get; set; }
        public string billing_address_zip_code { get; set; }
        public string billing_address_state { get; set; }
        public string billing_address_email { get; set; }
        public string billing_address_phone { get; set; }
        public string shipping_address_first_name { get; set; }
        public string shipping_address_last_name { get; set; }
        public string shipping_address_address1 { get; set; }
        public string shipping_address_address2 { get; set; }
        public string shipping_address_city { get; set; }
        public string shipping_address_zip_code { get; set; }
        public string shipping_address_state { get; set; }
        public string shipping_address_email { get; set; }
        public string shipping_address_phone { get; set; }
        public string ip_address { get; set; }

    }
    public class ProductModel
    {
        public long? id { get; set; }
        public int? quantity { get; set; }
    }
}
