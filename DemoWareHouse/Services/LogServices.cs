using DemoWareHouse.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DemoWareHouse.Services
{
    public class LogServices
    {
        private Connect connect = new Connect("WareHouse");

        private DateTime Modify_Date = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        public int SetLogUser(string User_ID, string IP4_Address, string Program_Log)
        {
            try
            {
                string query = $@"
                     DECLARE @numericPartLU BIGINT    
                    DECLARE @LUkey VARCHAR(15)    
                    SELECT @LUkey= MAX([Log_User_Serial]) FROM [Log_User]	
                    SET @numericPartLU = CAST(SUBSTRING(@LUkey, 3, LEN(@LUkey) - 2) AS INT)
                    SET @numericPartLU = @numericPartLU + 1
                    SET @LUkey = 'LU' + RIGHT('000000000000000' + CAST(@numericPartLU AS VARCHAR(13)), 13)
                    INSERT INTO Log_User
                    ([Log_User_Serial],[User_Serial_Key],[User_ID],[User_Password],[User_Name],[Group_Serial_Key],[Start_Date],[Leave_Date],[Login_Date]
                        ,[UUser_Serial_Key],[HostName],[IP4_Address],[Mac_address],[Program_Log],[TLLanguage]) 
                    SELECT @LUkey, User_Serial_Key, User_ID, User_Password, User_Name, Group_Serial_Key, Start_Date, Leave_Date,'{Modify_Date}', UUser_Serial_Key,
                    '', '{IP4_Address}', '', '{Program_Log}',TLLanguage FROM Data_User WHERE User_ID = '{User_ID}'";
                int check = connect.ExecuteCheckQuery(query);
                return check;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }



        public int SetLogMaterial(string BarCode, string IP4_Address, string Program_Log)
        {
            try
            {
                string query = $@"
                    DECLARE @numericPartLML BIGINT    
                    DECLARE @LMLkey VARCHAR(15)    
                    SELECT @LMLkey= MAX([Log_Material_Label_Serial]) FROM [Log_Material_Label]
                    SET @numericPartLML = CAST(SUBSTRING(@LMLkey, 3, LEN(@LMLkey) - 2) AS INT)
                    SET @numericPartLML = @numericPartLML + 1
                    SET @LMLkey = 'LM' + RIGHT('000000000000000' + CAST(@numericPartLML AS VARCHAR(13)), 13)
	
                      INSERT INTO Log_Material_Label
                  ([Log_Material_Label_Serial],[Material_Label_Serial],[Supplier],[Material_Name],[Color],[Size],[QTY],[Total_QTY],[Print_QTY],
                   [Print_Times],[Label_Status],[Order_No],[Roll],[Production],[Supplier_No],[Material_No],[Work_Order],[Material_Type],[BarCode]
                 ,[Modify_Date],[Print_Date],[User_Serial_Key],[HostName],[IP4_Address],[Mac_address],[Program_Log],[Arrival_QTY]) 
                 SELECT @LMLkey, Material_Label_Serial, Supplier, Material_Name, Color, Size, QTY, Total_QTY, Print_QTY, Print_Times, Label_Status,
                 Order_No, Roll, Production, Supplier_No,Material_No, Work_Order, Material_Type,'{BarCode}','{Modify_Date}',Print_Date, User_Serial_Key,
                 '','{IP4_Address}','','{Program_Log}',Arrival_QTY
                FROM Data_Material_Label WHERE [BarCode]='{BarCode}'";

                int check = connect.ExecuteCheckQuery(query);
                return check;

            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        public int SetLogStockIO(string Barcode, string IP4_Address, string Program_Log)
        {
            try
            {
                string query = $@"
                    DECLARE @numericPartLISO BIGINT    
                    DECLARE @LISOkey VARCHAR(15)    
                    SELECT @LISOkey= MAX([Log_Stock_In_Out_Serial]) FROM [Log_Stock_In_Out]	
                    SET @numericPartLISO = CAST(SUBSTRING(@LISOkey, 3, LEN(@LISOkey) - 2) AS INT)
                    SET @numericPartLISO = @numericPartLISO + 1
                    SET @LISOkey = 'LS' + RIGHT('000000000000000' + CAST(@numericPartLISO AS VARCHAR(13)), 13)
                   INSERT INTO Log_Stock_In_Out 
                    ([Log_Stock_In_Out_Serial], [Stock_In_Out_Serial], [Material_Label_Serial], [Barcode], [Stock_In_Out_Status], [Stock_In_No]
                        ,[Stock_Out_No], [QTY], [Print_Qty], [Storage_Serial], [Modify_Date], [User_Serial_Key], [HostName], [IP4_Address], [Mac_address], [Program_Log]) 
                    SELECT @LISOkey, Stock_In_Out_Serial, Material_Label_Serial,'{Barcode}',Stock_In_Out_Status, Stock_In_No, Stock_Out_No,QTY, Print_Qty,Storage_Serial,
                    '{Modify_Date}',User_Serial_Key, '','{IP4_Address}','','{Program_Log}'
                    FROM Data_Stock_In_Out WHERE Barcode ='{Barcode}'";

                int check = connect.ExecuteCheckQuery(query);
                return check;

            }
            catch (Exception ex)
            {
                return -1;
            }


        }
    }
}