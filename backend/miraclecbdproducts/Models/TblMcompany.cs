using System;
using System.Collections.Generic;

namespace MiraclecBDProducts.Models
{
    public partial class TblMcompany
    {
        public int Id { get; set; }
        public string ContactPerson { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CurrentPassword { get; set; }
        public string ShopUrl { get; set; }
    }
}
