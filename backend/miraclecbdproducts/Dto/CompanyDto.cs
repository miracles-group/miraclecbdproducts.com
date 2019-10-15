using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiraclecBDProducts.Dto
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Contact_Person { get; set; }
        public string Name { get; set; }
        public string Phone_Number { get; set; }
        public string Email_Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
