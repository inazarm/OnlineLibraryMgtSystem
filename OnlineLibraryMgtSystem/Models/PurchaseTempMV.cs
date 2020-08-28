using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineLibraryMgtSystem.Models
{
    public class PurchaseTempMV
    {
        public int PurTemID { get; set; }
        public int BookID { get; set; }
        public int Qty { get; set; }
        public double UnitPrice { get; set; }
    }
}