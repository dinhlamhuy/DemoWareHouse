using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWareHouse.Models
{
    public class Data_User
    {
        public string User_Serial_Key { get; set; }
        public string User_ID { get; set; }
        private string User_Password { get; set; }
        public string User_Name { get; set; }
        public string Group_Serial_Key { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime Leave_Date { get; set; }
        public DateTime Login_Date { get; set; }
        public string UUser_Serial_Key { get; set; }
        public string TLLanguage { get; set; }
    }
    public class LoginRequest
    {
        public string User_ID { get; set; }
        public string User_Password { get; set; }
    }

    public class RegisterRequest
    {
        public string User_ID { get; set; }
        public string User_Password { get; set; }
        public string User_Name { get; set; }
        public string Group_Serial_Key { get; set; }
        public string UUser_Serial_Key { get; set; }
        public string TLLanguage { get; set; }
        public string IP4_Address { get; set; }
    }
    public class LanguageRequest
    {
        public string User_ID { get; set; }
        public string TLLanguage { get; set; }
    }

}