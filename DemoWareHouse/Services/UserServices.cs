using DemoWareHouse.Models;
using DemoWareHouse.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace DemoWareHouse.Services
{
    public class UserServices
    {
        private Connect connect = new Connect("WareHouse");
        public List<Data_User> ListUser()
        {
            string query = "SELECT * FROM [Data_User]";
            DataTable result = connect.ExecuteQuery(query);
            List<Data_User> mang = new List<Data_User>();
            foreach (DataRow row in result.Rows)
            {
                Data_User Data_User = new Data_User()
                {
                    User_Serial_Key = row["User_Serial_Key"].ToString(),
                    User_ID = row["User_ID"].ToString(),
                    User_Name = row["User_Name"].ToString(),
                    Group_Serial_Key = row["Group_Serial_Key"].ToString(),
                    Start_Date = DateTime.Parse(row["Start_Date"].ToString()),
                    Leave_Date = DateTime.Parse(row["Leave_Date"].ToString()),
                    Login_Date = DateTime.Parse(row["Login_Date"].ToString()),
                    UUser_Serial_Key = row["UUser_Serial_Key"].ToString(),
                    TLLanguage = row["TLLanguage"].ToString()
                };
                mang.Add(Data_User);
            }
            return mang;
        }

        public Data_User GetUserId(string id)
        {
            string query = $"SELECT * FROM [Data_User] WHERE User_ID='{id}'";
            DataTable result = connect.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                Data_User user = new Data_User()
                {
                    User_Serial_Key = row["User_Serial_Key"].ToString(),
                    User_ID = row["User_ID"].ToString(),
                    User_Name = row["User_Name"].ToString(),
                    Group_Serial_Key = row["Group_Serial_Key"].ToString(),
                    Start_Date = DateTime.Parse(row["Start_Date"].ToString()),
                    Leave_Date = DateTime.Parse(row["Leave_Date"].ToString()),
                    Login_Date = DateTime.Parse(row["Login_Date"].ToString()),
                    UUser_Serial_Key = row["UUser_Serial_Key"].ToString(),
                    TLLanguage = row["TLLanguage"].ToString()
                };
                return user;
            }
            return null;
        }


        public List<Type_Language> ListLanguage(string lang)
        {
            string query = $"SELECT * FROM [Type_Language] WHERE TLLanguage = '{lang}'";
            DataTable result = connect.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                List<Type_Language> mang = new List<Type_Language>();
                foreach (DataRow row in result.Rows)
                {
                    Type_Language language = new Type_Language()
                    {
                        ObjectName = row["ObjectName"].ToString(),
                        TLLanguage = row["TLLanguage"].ToString(),
                        ObjectContent = row["ObjectContent"].ToString()
                    };
                    mang.Add(language);
                }
                return mang;
            } return null;
        }


        public List<Data_Group> ListDGroup()
        {
            string query = "SELECT * FROM [Data_Group]";
            DataTable result = connect.ExecuteQuery(query);
            List<Data_Group> mang = new List<Data_Group>();
            foreach (DataRow row in result.Rows)
            {
                Data_Group Data_Group = new Data_Group()
                {
                    Group_Serial_Key = row["Group_Serial_Key"].ToString(),
                    Group_ID = row["Group_ID"].ToString(),
                    Group_Name = row["Group_Name"].ToString(),
                    Start_Date = DateTime.Parse(row["Start_Date"].ToString()),
                    NoUse_Date = DateTime.Parse(row["NoUse_Date"].ToString()),
                    Modify_Date = DateTime.Parse(row["Modify_Date"].ToString()),
                    User_Serial_Key = row["User_Serial_Key"].ToString()
                };
                mang.Add(Data_Group);
            }
            return mang;
        }

        public string ChangeLang (string User_ID, string TLLanguage)
        {
            string query = $"UPDATE [Data_User] SET TLLanguage = '{TLLanguage}' WHERE User_ID ='{User_ID}'";
            try
            {
                connect.ExecuteNonQuery(query);
                return "Cập nhật thành công" + query;
            }
            catch (Exception ex)
            {
                return "Cập nhật thất bại: " + ex.Message;
            }

        }
    }
}