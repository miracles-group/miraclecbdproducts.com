﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiraclecBDProducts.Models
{
    public class CustomerModel
    {
        public string Cid { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Status { get; set; } //role
        public string CreateOn { get; set; }
    }
}
