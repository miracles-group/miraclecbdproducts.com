using System;
using System.Collections.Generic;

namespace MiraclecBDProducts.Models
{
    public partial class TblCompany
    {
        public int Id { get; set; }
        public string ContactPerson { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
