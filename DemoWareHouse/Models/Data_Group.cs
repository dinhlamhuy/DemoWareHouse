using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWareHouse.Models
{
    public class Data_Group
    {
        public string Group_Serial_Key { get; set; }
        public string Group_ID { get; set; }
        public string Group_Name { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime NoUse_Date { get; set; }
        public DateTime Modify_Date { get; set; }
        public string User_Serial_Key { get; set; }



    }
}