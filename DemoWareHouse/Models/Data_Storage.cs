using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWareHouse.Models
{
    public class Data_Storage
    {
        public string Storage_Serial { get; set; }
        public string Rack { get; set; }
        public double Max_Qty { get; set; }
        public double Min_Qty { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Material_Type { get; set; }
        public string Position { get; set; }
    }
}