using DemoWareHouse.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DemoWareHouse.Services
{
    public class LogUserServices
    {
        private Connect connect = new Connect("WareHouse");

        //Lấy danh sách Log_User
        public List<Log_User> listLogUser()
        {
            string query = "SELECT * FROM [Log_User] ";

            DataTable result = connect.ExecuteQuery(query);

            List<Log_User> mang = new List<Log_User>();
            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Log_User luser = new Log_User()
                    {
                        Log_User_Serial = row["Log_User_Serial"].ToString(),
                        User_Serial_Key = row["User_Serial_Key"].ToString(),
                        User_ID = row["User_ID"].ToString(),
                        User_Password = row["User_Password"].ToString(),
                        User_Name = row["User_Name"].ToString(),
                        Group_Serial_Key = row["Group_Serial_Key"].ToString(),
                        Start_Date = DateTime.Parse(row["Start_Date"].ToString()),
                        Leave_Date = DateTime.Parse(row["Leave_Date"].ToString()),
                        Login_Date = DateTime.Parse(row["Login_Date"].ToString()),
                        UUser_Serial_Key = row["UUser_Serial_Key"].ToString(),
                        HostName = row["HostName"].ToString(),
                        IP4_Address = row["IP4_Address"].ToString(),
                        Mac_address = row["Mac_address"].ToString(),
                        Program_Log = row["Program_Log"].ToString(),
                        TLLanguage = row["TLLanguage"].ToString(),
                    };
                    mang.Add(luser);
                }
                return mang;

            }
            else
            {
                return null;
            }

        }
    }
}