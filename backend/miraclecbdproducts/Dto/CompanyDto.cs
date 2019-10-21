using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiraclecBDProducts.Dto
{
    public class CompanyDto
    {
        public string contact_person { get; set; }
        public string name { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string current_password { get; set; }
        public string new_password { get; set; }
        public string confirm_new_password { get; set; }
    }
}
