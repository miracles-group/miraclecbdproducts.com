using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiraclecBDProducts.Models
{
    public class SyncProduct
    {
        public SyncProduct(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken jProduct = jObject["results"];
            id = (int)jProduct["id"];
            name = (string)jProduct["name"];
            description = (string)jProduct["description"];
            price = (float)jProduct["price"];
            slug = (string)jProduct["slug"];
            catelogy = (string)jProduct["catelogy"];
            sub_catelogy = (string)jProduct["sub_catelogy"];
            mg_of_cbd = (int)jProduct["mg_of_cbd"];
            ingredients = (string)jProduct["ingredients"];
            image_path = (string)jProduct["image_path"];
            coa_path = (string)jProduct["coa_path"];
        }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public string slug { get; set; }
        public string catelogy { get; set; }
        public string sub_catelogy { get; set; }
        public int mg_of_cbd { get; set; }
        public string ingredients { get; set; }
        public string image_path { get; set; }

        public string coa_path { get; set; }
    }
}
