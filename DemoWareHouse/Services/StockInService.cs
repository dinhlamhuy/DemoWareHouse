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
    public class StockInService
    {
        private Connect connect = new Connect("WareHouse");
        public dynamic getMaterialByRack(string Rack)
        {
            try
            {
                string query = $@"
                            DECLARE @Storage_Serial VarChar(20)

                               SELECT @Storage_Serial= Storage_Serial FROM Data_Storage WHERE Rack='{Rack}'
                                 IF @Storage_Serial IS NOT NULL
                                BEGIN
                                DECLARE @QTY Varchar(100) 
                                DECLARE @ROLL Varchar(100)
                                SELECT @QTY=  SUM(DSIO.QTY) ,@ROLL= SUM(CAST(SUBSTRING(Roll, CHARINDEX('/', Roll) + 1, LEN(Roll)) AS INT))
                                FROM [Data_Storage] DS 
                                INNER JOIN [Data_Stock_In_Out] DSIO ON DS.Storage_Serial = DSIO.Storage_Serial 
                                INNER JOIN [Data_Material_Label] DML ON DML.BarCode = DSIO.Barcode
                                WHERE Rack='{Rack}' AND
                                CAST(DSIO.Modify_Date AS DATE) = CAST(GETDATE() AS DATE)  AND Stock_In_Out_Status = 'In'
                                GROUP BY Rack
                                SELECT Top(1500)  @QTY AS SL,@ROLL AS SLRoll, DML.BarCode AS BarCode, Material_No, Rack, Supplier_No, Material_Name, Color,Size, 
                              DML.QTY AS QTY, DML.Print_QTY AS Print_QTY, DML.Total_QTY AS Total_QTY,Order_No, Roll, Supplier, Work_Order, DSIO.User_Serial_Key AS UserKey
                              FROM [Data_Storage] DS 
                              INNER JOIN [Data_Stock_In_Out] DSIO ON DS.Storage_Serial = DSIO.Storage_Serial 
                              INNER JOIN [Data_Material_Label] DML ON DML.BarCode = DSIO.Barcode
                              WHERE Rack='{Rack}'
                              END
                            ELSE
                            BEGIN
                                SELECT @Storage_Serial AS CheckRow
                            END
";
                DataTable result = connect.ExecuteQuery(query);
                if (result.Rows.Count > 0)
                {
                    if (!result.Columns.Contains("CheckRow"))
                    {
                        List<Data_Material_Label> mang = new List<Data_Material_Label>();

                        foreach (DataRow row in result.Rows)
                        {
                            string[] roll = row["Roll"].ToString().Split('/');
                            Data_Material_Label newDml = new Data_Material_Label()
                            {
                                BarCode = row["BarCode"].ToString(),
                                Material_No = row["Material_No"].ToString(),
                                Rack = row["Rack"].ToString(),
                                Supplier_No = row["Supplier_No"].ToString(),
                                Material_Name = row["Material_Name"].ToString(),
                                Color = row["Color"].ToString(),
                                Size = row["Size"].ToString(),
                                QTY = double.Parse(row["QTY"].ToString()),
                                Total_QTY = double.Parse(row["Total_QTY"].ToString()),
                                Print_QTY = row["Print_QTY"].ToString(),
                                Order_No = row["Order_No"].ToString(),
                                Roll = roll[0],
                                Supplier = row["Supplier"].ToString(),
                                Work_Order = row["Work_Order"].ToString(),
                                SL = row["SL"].ToString(),
                                SLRoll = row["SLRoll"].ToString(),
                                User_Serial_Key = row["UserKey"].ToString()
                            };
                            mang.Add(newDml);
                        }
                        return mang;

                    }
                    else
                    {
                        return "EmptyRacks";
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return "Fail " + ex;
            }


        }
        public dynamic OutRack(string Barcode, string User_ID, string IP4_Address)
        {
            string format = "yyyy-MM-dd HH:mm:ss.fff";
            DateTime currentDateTime = DateTime.Now;
            DateTime Modify_Date = DateTime.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);
            try
            {
                string query = $@"
                             DECLARE @QTY Varchar(100) 
                        DECLARE @BC Varchar(100) 
                        DECLARE @ROLL Varchar(100)
                        SELECT @BC= Storage_Serial FROM Data_Stock_In_Out WHERE Barcode= '{Barcode}'
                        UPDATE Data_Stock_In_Out SET Storage_Serial='', User_Serial_Key='{User_ID}', Modify_Date='{Modify_Date}' WHERE Barcode= '{Barcode}'
                                SELECT @QTY=  SUM(DSIO.QTY) ,@ROLL= SUM(CAST(SUBSTRING(Roll, CHARINDEX('/', Roll) + 1, LEN(Roll)) AS INT))
                                FROM [Data_Storage] DS 
                                INNER JOIN [Data_Stock_In_Out] DSIO ON DS.Storage_Serial = DSIO.Storage_Serial 
                                INNER JOIN [Data_Material_Label] DML ON DML.BarCode = DSIO.Barcode
                                WHERE DSIO.Storage_Serial=@BC AND
                                CAST(DSIO.Modify_Date AS DATE) = CAST(GETDATE() AS DATE)  AND Stock_In_Out_Status = 'In'
                                GROUP BY Rack    
                            SELECT @QTY AS SL, @ROLL AS SLRoll
";
                DataTable result = connect.ExecuteQuery(query);
                if (result.Rows.Count > 0)
                {

                    DataRow row = result.Rows[0];

                    LogServices lou = new LogServices();
                    int ghilog = lou.SetLogStockIO(Barcode, IP4_Address, "Update_Return_Data_Storage_Serial");
                    var sl = new
                    {
                        SL = row["SL"].ToString(),
                        SLRoll = row["SLRoll"].ToString(),
                    };

                    return sl;
                }
                return null;

            }
            catch (Exception ex)
            {
                return "Fail" + ex;
            }
        }

        public dynamic SetMaterialForRack(string Barcode, string Rack, string User_ID, string IP4_Address)
        {
            try
            {
                string format = "yyyy-MM-dd HH:mm:ss.fff";
                DateTime currentDateTime = DateTime.Now;
                DateTime Modify_Date = DateTime.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);
                string query = $@"
                 DECLARE @Storage_Serial VARCHAR(16)
                SELECT @Storage_Serial = Storage_Serial FROM Data_Storage WHERE Rack = '{Rack}'
                    DECLARE @AffectedRows INT
                    IF EXISTS (SELECT TOP 1 Barcode FROM Data_Stock_In_Out WHERE Storage_Serial = @Storage_Serial AND Barcode = '{Barcode}')
                BEGIN
                        SET @AffectedRows = 0
                    END
                    ELSE
                    BEGIN
                    UPDATE Data_Stock_In_Out SET Storage_Serial = @Storage_Serial, User_Serial_Key = '{User_ID}', Modify_Date='{Modify_Date}' WHERE Barcode = '{Barcode}' AND Stock_In_Out_Status = 'In'
                    SET @AffectedRows = @@ROWCOUNT                
                    END
                    SELECT @AffectedRows AS AffectedRows
                                ";
                DataTable result1 = connect.ExecuteQuery(query);
                if (result1.Rows.Count > 0)
                {
                    DataRow row = result1.Rows[0];
                    if (row["AffectedRows"].ToString() == "1")
                    {
                        LogServices lou = new LogServices();
                        int ghilog = lou.SetLogStockIO(Barcode, IP4_Address, "Update_Data_Storage_Serial");

                        string query2 = $@"
                DECLARE @Storage_Serial VARCHAR(16)
                                DECLARE @QTY Varchar(100) 
                                DECLARE @ROLL Varchar(100)
                                SELECT @QTY=  SUM(DSIO.QTY) ,@ROLL= SUM(CAST(SUBSTRING(Roll, CHARINDEX('/', Roll) + 1, LEN(Roll)) AS INT))
                                FROM [Data_Storage] DS 
                                INNER JOIN [Data_Stock_In_Out] DSIO ON DS.Storage_Serial = DSIO.Storage_Serial 
                                INNER JOIN [Data_Material_Label] DML ON DML.BarCode = DSIO.Barcode
                                WHERE Rack='{Rack}' AND
                                CAST(DSIO.Modify_Date AS DATE) = CAST(GETDATE() AS DATE)  AND Stock_In_Out_Status = 'In'
                                GROUP BY Rack
                SELECT @Storage_Serial = Storage_Serial FROM Data_Storage WHERE Rack = '{Rack}'
                SELECT @QTY AS SL,@ROLL AS SLRoll, DML.BarCode AS BarCode, Material_No, Supplier_No, Material_Name, Color, Size,
                        DML.QTY AS QTY, DML.Print_QTY AS Print_QTY, DML.Total_QTY AS Total_QTY, Order_No, Roll, Supplier, Work_Order, DSIO.User_Serial_Key AS UserKey
                FROM [Data_Stock_In_Out] DSIO
                INNER JOIN [Data_Material_Label] DML ON DML.BarCode = DSIO.Barcode
                WHERE DSIO.Barcode = '{Barcode}'
                ";

                        DataTable result2 = connect.ExecuteQuery(query2);
                        if (result2.Rows.Count > 0)
                        {
                            DataRow rows = result2.Rows[0];
                            string[] roll = rows["Roll"].ToString().Split('/');
                            Data_Material_Label newDml = new Data_Material_Label()
                            {
                                BarCode = rows["BarCode"].ToString(),
                                Material_No = rows["Material_No"].ToString(),
                                Rack = Rack,
                                Supplier_No = rows["Supplier_No"].ToString(),
                                Material_Name = rows["Material_Name"].ToString(),
                                Color = rows["Color"].ToString(),
                                Size = rows["Size"].ToString(),
                                QTY = double.Parse(rows["QTY"].ToString()),
                                Total_QTY = double.Parse(rows["Total_QTY"].ToString()),
                                Print_QTY = rows["Print_QTY"].ToString(),
                                Order_No = rows["Order_No"].ToString(),
                                Roll = roll[0],
                                SL = rows["SL"].ToString(),
                                SLRoll = rows["SLRoll"].ToString(),
                                Supplier = rows["Supplier"].ToString(),
                                Work_Order = rows["Work_Order"].ToString(),
                                User_Serial_Key = rows["UserKey"].ToString()
                            };
                            return newDml;
                        }
                    }

                }

                return null;
            }
            catch (Exception ex)
            {
                return "Fail" + ex;
            }
        }

        public dynamic getExportInList(string Status, string Order_No, string Material_No, string Supplier, DateTime Date_Start, DateTime Date_End)
        {
            try
            {
                string chuoisql = "";
                string query = "";
                if (!string.IsNullOrWhiteSpace(Order_No))
                {
                    chuoisql += $" AND Order_No = '{Order_No}'";
                }
                if (!string.IsNullOrWhiteSpace(Material_No))
                {
                    chuoisql += $" AND Material_No = '{Material_No}'";
                }
                if (!string.IsNullOrWhiteSpace(Supplier))
                {
                    chuoisql += @" AND Supplier = '" + Supplier + "'";
                }
                query = $@"SELECT Rack, Order_No, Supplier, Supplier_No, Print_Date,Color, Size,Production, Work_Order, Material_No, Material_Name,
                                    DML.Print_QTY as PrintQTY, DSIO.QTY AS SL, Roll, DSIO.User_Serial_Key AS UserKey
                                    FROM Data_Material_Label AS DML INNER JOIN Data_Stock_In_Out AS DSIO ON DML.BarCode = DSIO.Barcode INNER JOIN Data_Storage DS ON 
                                    DS.Storage_Serial = DSIO.Storage_Serial
                                    WHERE Stock_In_Out_Status = '{Status}' AND DSIO.Modify_Date BETWEEN '{Date_Start}' AND '{Date_End}'" + chuoisql + " ORDER BY Order_No, Material_No, Rack";

                DataTable result = connect.ExecuteQuery(query);
                if (result.Rows.Count > 0)
                {
                    List<ExportList> mang = new List<ExportList>();
                    string mavt = "";
                    string phieu = "";

                    ExportList temp = new ExportList();
                    foreach (DataRow row in result.Rows)
                    {
                        string[] roll = row["Roll"].ToString().Split('/');
                        string[] rolltemp = temp.Roll.ToString().Split('/');
                        string[] dv = row["PrintQTY"].ToString().Split(' ');

                        if (mavt == row["Material_No"].ToString() && phieu == row["Order_No"].ToString())
                        {
                            temp.Roll = double.Parse(rolltemp[0].ToString()) + double.Parse(roll[0].ToString());
                            temp.QTY = double.Parse(temp.QTY.ToString()) + double.Parse(row["SL"].ToString());
                        }
                        else
                        {
                            if (mavt != "" && phieu != "")
                            {
                                mang.Add(temp);
                            }
                            temp = new ExportList();
                            mavt = row["Material_No"].ToString();
                            phieu = row["Order_No"].ToString();
                            temp.Rack = row["Rack"].ToString();
                            temp.Color = row["Color"].ToString();
                            temp.Size = row["Size"].ToString();
                            temp.Production = row["Production"].ToString();
                            temp.User_Serial_Key = row["UserKey"].ToString();
                            temp.Supplier = row["Supplier"].ToString();
                            temp.Supplier_No = row["Supplier_No"].ToString();
                            temp.Order_No = row["Order_No"].ToString();
                            temp.Work_Order = row["Work_Order"].ToString();
                            temp.Material_No = row["Material_No"].ToString();
                            temp.Material_Name = row["Material_Name"].ToString();
                            temp.Roll = double.Parse(roll[0].ToString());
                            temp.QTY = double.Parse(row["SL"].ToString());
                            temp.chitietkien = null;
                            temp.Print_QTY = dv[1].ToString();
                            temp.Print_Date = DateTime.Parse(row["Print_Date"].ToString()).ToString("dd/MM/yyyy");
                        }

                    }

                    if (mavt != "")
                    {

                        mang.Add(temp);
                    }

                    return mang;


                }
                return null;
            }
            catch (Exception ex)
            {
                return "Fail " + ex;
            }
        }


    }
}