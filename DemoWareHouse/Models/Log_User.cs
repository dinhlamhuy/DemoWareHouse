using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWareHouse.Models
{
    public class Log_User
    {
        public string Log_User_Serial { get; set; }
        public string User_Serial_Key { get; set; }
        public string User_ID { get; set; }
        public string User_Password { get; set; }
        public string User_Name { get; set; }
        public string Group_Serial_Key { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime Leave_Date { get; set; }
        public DateTime Login_Date { get; set; }
        public string UUser_Serial_Key { get; set; }
        public string HostName { get; set; }
        public string IP4_Address { get; set; }
        public string Mac_address { get; set; }
        public string Program_Log { get; set; }
        public string TLLanguage { get; set; }
    }
    public class LogUserRequest
    {
        public string User_ID { get; set; }
        public string HostName { get; set; }
        public string IP4_Address { get; set; }
        public string Mac_address { get; set; }
        public string Program_Log { get; set; }
       // public string User_Serial_Key { get; set; }
        //public string User_Password { get; set; }
        //public string User_Name { get; set; }
        //public string Group_Serial_Key { get; set; }
        //public DateTime Start_Date { get; set; }
        //public DateTime Leave_Date { get; set; }
        //public DateTime Login_Date { get; set; }
        //public string UUser_Serial_Key { get; set; }
        //public string TLLanguage { get; set; }
    }
}