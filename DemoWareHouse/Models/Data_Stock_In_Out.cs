using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWareHouse.Models
{
    public class Data_Stock_In_Out
    {
        public string Stock_In_Out_Serial { get; set; }
        public string Material_Label_Serial { get; set; }
        public string Barcode { get; set; }
        public string Stock_In_Out_Status { get; set; }
        public string Stock_In_No { get; set; }
        public string Stock_Out_No { get; set; }
        public double? QTY { get; set; }
        public string Print_Qty { get; set; }
        public string Storage_Serial { get; set; }
        public DateTime? Modify_Date { get; set; }
        public string User_Serial_Key { get; set; }
        
    }
    public class StockOutRequest
    {
        public string User_ID { get; set; }
        public double QTY { get; set; }
        public double? Tong_QTY { get; set; }
        public string IP4_Address { get; set; }
        public string Barcode { get; set; }
        public string Rack { get; set; }
    }
    public class GetMaterialNoRequest
    {
        public string Material_No { get; set; }
        public DateTime Date_Start { get; set; }
        public DateTime Date_End { get; set; }
    }
    public class GetExportListRequest
    {
        public string Status { get; set; }
        public string Order_No { get; set; }
        public string Material_No { get; set; }
        public string Supplier { get; set; }
        public DateTime Date_Start { get; set; }
        public DateTime Date_End { get; set; }
    }

    public class ExportList
    {
        public string Rack { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Production { get; set; }
        public string User_Serial_Key { get; set; }
        public string Supplier { get; set; }
        public string Supplier_No { get; set; }

        public string Order_No { get; set; }

        public string Work_Order { get; set; }

        public string Material_No { get; set; }

        public string Material_Name { get; set; }

        public double Roll { get; set; }

        public string Print_QTY { get; set; }

        public double QTY { get; set; }

        public string chitietkien { get; set; }
        public string Print_Date { get; set; }
    }
}