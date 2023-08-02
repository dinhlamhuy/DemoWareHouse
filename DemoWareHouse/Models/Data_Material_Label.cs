using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWareHouse.Models
{
    public class Data_Material_Label
    {
        public string Material_Label_Serial { get; set; }
        public string Supplier { get; set; }
        public string Material_Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double QTY { get; set; }
        public double Total_QTY { get; set; }
        public string Print_QTY { get; set; }
        public int Print_Times { get; set; }
        public string Label_Status { get; set; }
        public string Order_No { get; set; }
        public string Roll { get; set; }
        public string Production { get; set; }
        public string Supplier_No { get; set; }
        public string Material_No { get; set; }
        public string Work_Order { get; set; }
        public string Material_Type { get; set; }
        public string BarCode { get; set; }
        public DateTime Modify_Date { get; set; }
        public DateTime Print_Date { get; set; }
        public string User_Serial_Key { get; set; }
        public string Rack { get; set; }
        public string SL { get; set; }
        public string SLRoll { get; set; }
        public double Arrival_QTY { get; set; }

    }
}