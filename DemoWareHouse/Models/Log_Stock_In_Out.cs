using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWareHouse.Models
{
    public class Log_Stock_In_Out
    {
        public string Log_Stock_In_Out_Serial { get; set; }
        public string Stock_In_Out_Serial { get; set; }
        public string Material_Label_Serial { get; set; }
        public string Barcode { get; set; }
        public string Stock_In_Out_Status { get; set; }
        public string Stock_In_No { get; set; }
        public string Stock_Out_No { get; set; }
        public double QTY { get; set; }
        public string Print_Qty { get; set; }
        public string Storage_Serial { get; set; }
        public DateTime Modify_Date { get; set; }
        public string User_Serial_Key { get; set; }
        public string HostName { get; set; }
        public string IP4_Address { get; set; }
        public string Mac_address { get; set; }
        public string Program_Log { get; set; }
    }

    public class LogStockOutRequest
    {
        //public string Log_Stock_In_Out_Serial { get; set; }
        //public string Stock_In_Out_Serial { get; set; }
        //public string Material_Label_Serial { get; set; }
        public string Barcode { get; set; }
        //public string Stock_In_Out_Status { get; set; }
        //public string Stock_In_No { get; set; }
        //public string Stock_Out_No { get; set; }
        //public double QTY { get; set; }
        //public string Print_Qty { get; set; }
        //public string Storage_Serial { get; set; }
        //public DateTime Modify_Date { get; set; }
        //public string User_Serial_Key { get; set; }
        public string HostName { get; set; }
        public string IP4_Address { get; set; }
        public string Mac_address { get; set; }
        public string Program_Log { get; set; }
    }

}