using DemoWareHouse.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Globalization;

namespace DemoWareHouse.Services
{
    public class AuthServices
    {
        private Connect connect = new Connect("WareHouse");



        public dynamic LoginService(string User_ID, string Password)
        {
            string query = $@"SELECT * FROM [Data_User] WHERE User_ID = '{User_ID}'";
            DataTable result = connect.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                string savedPassword = row["User_Password"].ToString();

                if (savedPassword == Password.ToUpper())
                {
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
                else
                {
                    //return "Mật khẩu không đúng";
                    return 2;
                }
            }
            else
            {
                //return "Không tìm thấy tài khoản của bạn";
                return 3;
            }
        }

        public dynamic RegisterService(RegisterRequest req)
        {
            string check = $"SELECT User_ID FROM [Data_User] WHERE User_ID = '{req.User_ID}'";
            DataTable kq = connect.ExecuteQuery(check);
            if (kq.Rows.Count > 0)
            {
                return "Account already exists";
            }
            else
            {
                try
                {
                    //Tìm user_serial_key tiếp theo theo thứ tự tăng dần
                    string selectMax = "SELECT MAX(User_Serial_Key) AS MaxKey FROM [Data_User]";
                    DataTable timmaxkey = connect.ExecuteQuery(selectMax);
                    DataRow rowKey = timmaxkey.Rows[0];
                    string maxKey = rowKey["MaxKey"].ToString();
                    string prefix = maxKey.Substring(0, 2);
                    string numericPart = maxKey.Substring(2);
                    int number = int.Parse(numericPart);
                    number++;
                    string user_serial_key = prefix + number.ToString("D8");
                    // DateTime
                    string format = "yyyy-MM-dd HH:mm:ss.fff";
                    DateTime currentDateTime = DateTime.Now;
                    string Leave_Date_String = "2099-12-31 00:00:00.000";

                    DateTime formattedStart_Date = DateTime.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);
                    DateTime formattedLeave_Date = DateTime.ParseExact(Leave_Date_String, format, CultureInfo.InvariantCulture);
                    DateTime formattedLogin_Date = DateTime.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);
                    string Start_Date = formattedStart_Date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string Leave_Date = formattedLeave_Date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string Login_Date = formattedLogin_Date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string query = $@"INSERT INTO [Data_User] (User_Serial_Key, User_ID, User_Password, User_Name, 
                        Group_Serial_Key, Start_Date, Leave_Date, Login_Date, UUser_Serial_Key, TLLanguage) 
                        VALUES('{user_serial_key}', '{req.User_ID}', '{req.User_Password.ToUpper()}', N'{req.User_Name}',
                        'DG00000008','{Start_Date}', '{Leave_Date}', '{Login_Date}', '{user_serial_key}', '{req.TLLanguage}');

                    DECLARE @numericPartLU BIGINT    
                    DECLARE @LUkey VARCHAR(15)    
                    SELECT @LUkey= MAX([Log_User_Serial]) FROM [Log_User]	
                    SET @numericPartLU = CAST(SUBSTRING(@LUkey, 3, LEN(@LUkey) - 2) AS INT)
                    SET @numericPartLU = @numericPartLU + 1
                    SET @LUkey = 'LU' + RIGHT('000000000000000' + CAST(@numericPartLU AS VARCHAR(13)), 13)
                    INSERT INTO Log_User
                    ([Log_User_Serial],[User_Serial_Key],[User_ID],[User_Password],[User_Name],[Group_Serial_Key],[Start_Date],[Leave_Date],[Login_Date]
                        ,[UUser_Serial_Key],[HostName],[IP4_Address],[Mac_address],[Program_Log],[TLLanguage]) VALUES
                    (@LUkey, '{user_serial_key}', '{req.User_ID}', '{req.User_Password.ToUpper()}', N'{req.User_Name}',
                    'DG00000008','{Start_Date}', '{Leave_Date}', '{Login_Date}', '{user_serial_key}', '',
                        '{req.IP4_Address}', '', 'Function : AddUser()', '{req.TLLanguage}')";
                    connect.ExecuteNonQuery(query);
                    return "Success";
                }
                catch(Exception ex)
                {
                    return "Fail " + ex.Message ;
                }      
            }
        }

        public int ChangePassword(string user_ID, string oldPassword, string newPassword)
        {
            string query = $"SELECT User_Password FROM [Data_User] WHERE User_ID = '{user_ID}'";
            DataTable result = connect.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                string savedPassword = row["User_Password"].ToString();

                if (savedPassword.ToUpper() == oldPassword.ToUpper())
                {
                    string updateQuery = $"UPDATE [Data_User] SET User_Password = '{newPassword.ToUpper()}' WHERE User_ID = '{user_ID}'";
                    connect.ExecuteNonQuery(updateQuery);
                    return 0;
                }
                else
                {
                    return 2;
                }
            }
            return 1;

        }

    }
}