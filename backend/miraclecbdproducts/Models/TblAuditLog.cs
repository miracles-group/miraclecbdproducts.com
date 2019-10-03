using System;
using System.Collections.Generic;

namespace MiraclecBDProducts.Models
{
    public partial class TblAuditLog
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Message { get; set; }
    }
}
