using DemoWareHouse.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using DemoWareHouse.Services;
namespace DemoWareHouse.Services
{
    public class StockOutServices
    {
        private Connect connect = new Connect("WareHouse");
        //lấy danh sách của bảng data Stock In Out
        public List<Data_Stock_In_Out> GetListStockIO(string BarCode)
        {
            string query = $"SELECT * FROM [Data_Stock_In_Out] WHERE [BarCode] = '{BarCode}'";
            DataTable result = connect.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                List<Data_Stock_In_Out> mang = new List<Data_Stock_In_Out>();
                foreach (DataRow row in result.Rows)
                {
                    Data_Stock_In_Out dsio = new Data_Stock_In_Out()
                    {
                        Stock_In_Out_Serial = row["Stock_In_Out_Serial"].ToString(),
                        Material_Label_Serial = row["Material_Label_Serial"].ToString(),
                        Barcode = row["Barcode"].ToString(),
                        Stock_In_Out_Status = row["Stock_In_Out_Status"].ToString(),
                        Stock_In_No = row["Stock_In_No"].ToString(),
                        Stock_Out_No = row["Stock_Out_No"].ToString(),
                        QTY = double.Parse(row["QTY"].ToString()),
                        Print_Qty = row["Print_Qty"].ToString(),
                        Storage_Serial = row["Storage_Serial"].ToString(),
                        Modify_Date = DateTime.Parse(row["Modify_Date"].ToString()),
                        User_Serial_Key = row["User_Serial_Key"].ToString(),

                    };
                    mang.Add(dsio);
                }
                return mang;
            }
            return null;
        }

        //lấy danh sách của bảng data material label 
        public dynamic GetListMaterialLabel(string BarCode)
        {
            string query = $"SELECT * FROM [Data_Material_Label] WHERE [BarCode] = '{BarCode}'";
            DataTable result = connect.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                List<Data_Material_Label> mang = new List<Data_Material_Label>();
                foreach (DataRow row in result.Rows)
                {
                    Data_Material_Label dml = new Data_Material_Label()
                    {
                        Material_Label_Serial = row["Material_Label_Serial"].ToString(),
                        Supplier = row["Supplier"].ToString(),
                        Material_Name = row["Material_Name"].ToString(),
                        Color = row["Color"].ToString(),
                        Size = row["Size"].ToString(),
                        QTY = double.Parse(row["QTY"].ToString()),
                        Total_QTY = double.Parse(row["Total_QTY"].ToString()),
                        Print_QTY = row["Print_QTY"].ToString(),
                        Print_Times = int.Parse(row["Print_Times"].ToString()),
                        Label_Status = row["Label_Status"].ToString(),
                        Order_No = row["Order_No"].ToString(),
                        Roll = row["Roll"].ToString(),
                        Production = row["Production"].ToString(),
                        Supplier_No = row["Supplier_No"].ToString(),
                        Material_No = row["Material_No"].ToString(),
                        Work_Order = row["Work_Order"].ToString(),
                        Material_Type = row["Material_Type"].ToString(),
                        BarCode = row["BarCode"].ToString(),
                        Modify_Date = DateTime.Parse(row["Modify_Date"].ToString()),
                        Print_Date = DateTime.Parse(row["Print_Date"].ToString()),
                        User_Serial_Key = row["User_Serial_Key"].ToString(),
                        Arrival_QTY = double.Parse(row["Arrival_QTY"].ToString())
                    };
                    mang.Add(dml);
                }
                return mang;
            }
            return null;
        }


        public dynamic GetStockIO(string BarCode)
        {
            string query = $@"
                SELECT TOP 1 sio.Stock_In_Out_Serial, sio.Material_Label_Serial, sio.Barcode, sio.Stock_In_Out_Status, sio.Stock_In_No,
                    sio.Stock_Out_No, sio.QTY AS SIO_QTY, sio.Print_Qty, sio.Storage_Serial, sio.Modify_Date AS SIO_Modify_Date,
                    sio.User_Serial_Key,
                    dml.Material_Label_Serial, dml.Supplier, dml.Material_Name, dml.Color, dml.Size, dml.QTY AS DML_QTY,
                    dml.Total_QTY, dml.Print_QTY, dml.Print_Times, dml.Label_Status, dml.Order_No, dml.Roll, dml.Production,
                    dml.Supplier_No, dml.Material_No, dml.Work_Order, dml.Material_Type, dml.BarCode, dml.Modify_Date AS DML_Modify_Date,
                    dml.Print_Date, dml.User_Serial_Key, dml.Arrival_QTY
                FROM [Data_Stock_In_Out] sio
                INNER JOIN [Data_Material_Label] dml ON sio.Barcode = dml.BarCode
                WHERE sio.Barcode = '{BarCode}' AND sio.Stock_In_Out_Status = 'In'";

            try
            {
                DataTable results = connect.ExecuteQuery(query);
                if (results.Rows.Count > 0)
                {
                    DataRow row = results.Rows[0];
                    Data_Stock_In_Out sio = new Data_Stock_In_Out()
                    {
                        Stock_In_Out_Serial = row["Stock_In_Out_Serial"].ToString(),
                        Material_Label_Serial = row["Material_Label_Serial"].ToString(),
                        Barcode = row["Barcode"].ToString(),
                        Stock_In_Out_Status = row["Stock_In_Out_Status"].ToString(),
                        Stock_In_No = row["Stock_In_No"].ToString(),
                        Stock_Out_No = row["Stock_Out_No"].ToString(),
                        QTY = double.Parse(row["SIO_QTY"].ToString()),
                        Print_Qty = row["Print_Qty"].ToString(),
                        Storage_Serial = row["Storage_Serial"].ToString(),
                        Modify_Date = DateTime.Parse(row["SIO_Modify_Date"].ToString()),
                        User_Serial_Key = row["User_Serial_Key"].ToString(),
                    };

                    Data_Material_Label dml = new Data_Material_Label()
                    {
                        Material_Label_Serial = row["Material_Label_Serial"].ToString(),
                        Supplier = row["Supplier"].ToString(),
                        Material_Name = row["Material_Name"].ToString(),
                        Color = row["Color"].ToString(),
                        Size = row["Size"].ToString(),
                        QTY = double.Parse(row["DML_QTY"].ToString()),
                        Total_QTY = double.Parse(row["Total_QTY"].ToString()),
                        Print_QTY = row["Print_QTY"].ToString(),
                        Print_Times = int.Parse(row["Print_Times"].ToString()),
                        Label_Status = row["Label_Status"].ToString(),
                        Order_No = row["Order_No"].ToString(),
                        Roll = row["Roll"].ToString(),
                        Production = row["Production"].ToString(),
                        Supplier_No = row["Supplier_No"].ToString(),
                        Material_No = row["Material_No"].ToString(),
                        Work_Order = row["Work_Order"].ToString(),
                        Material_Type = row["Material_Type"].ToString(),
                        BarCode = row["BarCode"].ToString(),
                        Modify_Date = DateTime.Parse(row["DML_Modify_Date"].ToString()),
                        Print_Date = DateTime.Parse(row["Print_Date"].ToString()),
                        User_Serial_Key = row["User_Serial_Key"].ToString(),
                        Arrival_QTY = double.Parse(row["Arrival_QTY"].ToString())
                    };

                    var response = new
                    {
                        dml = dml,
                        sio = sio
                    };

                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                return "Fail " + ex;
            }
        }

        public dynamic StockOutALL(string Barcode, string User_ID, string IP4_Address)
        {
            try
            {
                string queryso = $"SELECT Top 1 * FROM [Data_Stock_In_Out] WHERE [Barcode] = '{Barcode}' AND [Stock_In_Out_Status] = 'In'";
                DataTable resultso = connect.ExecuteQuery(queryso);
                if (resultso.Rows.Count > 0)
                {
                    string format = "yyyy-MM-dd HH:mm:ss.fff";
                    DateTime currentDateTime = DateTime.Now;
                    DateTime Modify_Date = DateTime.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);

                    string updateso = $@"UPDATE [Data_Stock_In_Out] SET [Stock_In_Out_Status] = 'Out',
                         [Storage_Serial] ='', [Modify_Date]='{Modify_Date}' , [User_Serial_Key]='{User_ID}'  
                         WHERE [Barcode] = '{Barcode}';
                         UPDATE [Data_Material_Label] SET [Modify_Date] = '{Modify_Date}',  [User_Serial_Key] = '{User_ID}'  WHERE [BarCode] = '{Barcode}';
                            DECLARE @QTY Varchar(100) SELECT @QTY= SUM(QTY) 
                            FROM Data_Stock_In_Out WHERE CAST(Modify_Date AS DATE) = CAST(GETDATE() AS DATE)  AND Stock_In_Out_Status = 'Out'
                              AND User_Serial_Key = '{User_ID}' GROUP BY User_Serial_Key
                            SELECT TOP 1 @QTY as SL, sio.Stock_In_Out_Serial, sio.Material_Label_Serial, sio.Barcode, sio.Stock_In_Out_Status, sio.Stock_In_No,
                            sio.Stock_Out_No, sio.QTY AS SIO_QTY, sio.Print_Qty, sio.Storage_Serial, sio.Modify_Date AS SIO_Modify_Date,
                            sio.User_Serial_Key,
                            dml.Material_Label_Serial, dml.Supplier, dml.Material_Name, dml.Color, dml.Size, dml.QTY AS DML_QTY,
                            dml.Total_QTY, dml.Print_QTY, dml.Print_Times, dml.Label_Status, dml.Order_No, dml.Roll, dml.Production,
                            dml.Supplier_No, dml.Material_No, dml.Work_Order, dml.Material_Type, dml.BarCode, dml.Modify_Date AS DML_Modify_Date,
                            dml.Print_Date, dml.User_Serial_Key, dml.Arrival_QTY
                        FROM [Data_Stock_In_Out] sio
                        INNER JOIN [Data_Material_Label] dml ON sio.Barcode = dml.BarCode
                        WHERE sio.Barcode = '{Barcode}'";

                    DataTable results = connect.ExecuteQuery(updateso);
                    if (results.Rows.Count > 0)
                    {
                        LogServices lou = new LogServices();
                        int ghilog = lou.SetLogStockIO(Barcode, IP4_Address, "Update_Data_Stock_In_Out");
                        DataRow row = results.Rows[0];
                        Data_Material_Label dml = new Data_Material_Label()
                        {
                            Material_Label_Serial = row["Material_Label_Serial"].ToString(),
                            Supplier = row["Supplier"].ToString(),
                            Material_Name = row["Material_Name"].ToString(),
                            Color = row["Color"].ToString(),
                            Size = row["Size"].ToString(),
                            QTY = double.Parse(row["DML_QTY"].ToString()),
                            Total_QTY = double.Parse(row["Total_QTY"].ToString()),
                            Print_QTY = row["Print_QTY"].ToString(),
                            Print_Times = int.Parse(row["Print_Times"].ToString()),
                            Label_Status = row["Label_Status"].ToString(),
                            Order_No = row["Order_No"].ToString(),
                            Roll = row["Roll"].ToString(),
                            Production = row["Production"].ToString(),
                            Supplier_No = row["Supplier_No"].ToString(),
                            Material_No = row["Material_No"].ToString(),
                            Work_Order = row["Work_Order"].ToString(),
                            Material_Type = row["Material_Type"].ToString(),
                            BarCode = row["BarCode"].ToString(),
                            Modify_Date = DateTime.Parse(row["DML_Modify_Date"].ToString()),
                            Print_Date = DateTime.Parse(row["Print_Date"].ToString()),
                            User_Serial_Key = row["User_Serial_Key"].ToString(),
                            SL = row["SL"].ToString(),
                            Arrival_QTY = double.Parse(row["Arrival_QTY"].ToString())
                        };

                        var response = new
                        {
                            dml = dml,
                            ghilog = ghilog

                        };

                        return response;
                    }

                    return null;
                }
                else
                {
                    return "Không tìm thấy vật tư hoặc đã xuất từ trước rồi ";
                }
            }
            catch (Exception ex)
            {
                return "Fail " + ex;

            }

        }


        // Tong_QTY là số lượng của vật tư (QTY)// Còn QTY là số lượng vật tư lấy ra 
        public dynamic StockOut(string Barcode, string Tong_QTY, string QTY, string User_ID, string IP4_Address)
        {
            string format = "yyyy-MM-dd HH:mm:ss.fff";
            DateTime currentDateTime = DateTime.Now;
            DateTime Modify_Date = DateTime.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);

            double QTY_conlai = double.Parse(Tong_QTY.ToString()) - double.Parse(QTY.ToString());
            try
            {
                string queryso = $"SELECT Top 1 * FROM [Data_Stock_In_Out] WHERE [BarCode] = '{Barcode}' AND [Stock_In_Out_Status] = 'In'";
                DataTable resultso = connect.ExecuteQuery(queryso);
                if (resultso.Rows.Count > 0 && QTY_conlai > 0)
                {
                    string insertOut = $@"
                    --update số lượng QTY cho 2 bảng
                    UPDATE [Data_Stock_In_Out]  SET [QTY] = '{QTY_conlai}', [Modify_Date] = '{Modify_Date}', [User_Serial_Key] = '{User_ID}' WHERE [Barcode] = '{Barcode}';
                    UPDATE [Data_Material_Label] SET [QTY] = '{QTY_conlai}', [Modify_Date] = '{Modify_Date}',  [User_Serial_Key] = '{User_ID}'  WHERE [BarCode] = '{Barcode}';
                    -- Xử lý ngày tháng năm để lấy 2 ký tự cuối vd: 2023-> 23 // 6 -> 06 / 12 -> 12
                    DECLARE @Year CHAR(2) = RIGHT(YEAR(GETDATE()), 2)
                    DECLARE @Month CHAR(2) = RIGHT('0' + CAST(MONTH(GETDATE()) AS VARCHAR(2)), 2)
                    DECLARE @Day CHAR(2) = RIGHT('0' + CAST(DAY(GETDATE()) AS VARCHAR(2)), 2)
                    -- lưu trữ những ký tự đầu vi dụ như ML, DS và Material_Type là kiểu header của barcode
                    DECLARE @Material_Type VARCHAR(4)
                    DECLARE @headerPartMLS VARCHAR(2)
                    DECLARE @headerPartSIOL VARCHAR(2)
                    -- Lưu trữ phần  phần số để tăng lên một đơn vị
                    DECLARE @numericPartBC BIGINT
                    DECLARE @numericPartMLS BIGINT
                    DECLARE @numericPartSIOL BIGINT
                    -- Kiểm tra xem Material_Label_Serial của 2 bảng bên nào có giá trị lớn hơn
                    DECLARE @DML_Material_Label_Serial VARCHAR(16)
                    DECLARE @SIO_Material_Label_Serial VARCHAR(16)
                    -- lưu giá trị đã tăng lên để insert
                    DECLARE @BCkeyCheck VARCHAR(16)
                    DECLARE @BCkey VARCHAR(16)
                    DECLARE @MLSkey VARCHAR(15)
                    DECLARE @SIOLkey VARCHAR(15)
                    --set giá trị cho Material_Type
                    SELECT @Material_Type= Material_Type FROM [Data_Material_Label] WHERE BarCode ='{Barcode}'

                    SET @BCkeyCheck = @Material_Type + @Year + @Month + @Day 
                    SET @BCkey =@Material_Type + @Year + @Month + @Day +'000000'

                    SELECT @BCkey = CASE WHEN EXISTS (SELECT 1 FROM [Data_Material_Label] WHERE Barcode LIKE @BCkeyCheck + '%')
                   THEN (SELECT MAX(Barcode) FROM [Data_Material_Label] WHERE Barcode LIKE @BCkeyCheck + '%')
                   ELSE @BCkey
                     END
                    -- chọn giá trị lớn nhất Stock_In_Out_Serial,  Barcode, Material_Label_Serial ở bảng Data_Stock_In_Out
                    SELECT @SIOLkey= MAX(Stock_In_Out_Serial), @SIO_Material_Label_Serial= MAX(Data_Stock_In_Out.Material_Label_Serial)
                    FROM Data_Stock_In_Out 
                    -- chọn giá trị lớn nhất  Barcode, Material_Label_Serial ở bảng Data_Material_Label
                    SELECT 
                    @DML_Material_Label_Serial= MAX(Data_Material_Label.Material_Label_Serial) FROM Data_Material_Label
                    --lưu lại giá trị lớn nhất cho MLSkey, BCkey trong 2 bảng
                    SET @MLSkey = CASE
                        WHEN @DML_Material_Label_Serial > @SIO_Material_Label_Serial THEN @DML_Material_Label_Serial
                        ELSE @SIO_Material_Label_Serial
                    END
                    --Xử lý về phần barcode (đã tăng lên 1 giá trị)
                    SET @numericPartBC = RIGHT(@BCkey, 6)
                    SET @numericPartBC = @numericPartBC + 1
                    SET @numericPartBC = RIGHT('000000' +CAST(@numericPartBC AS VARCHAR(6)) ,6)
                    SET @BCkey = @Material_Type + @Year + @Month + @Day + RIGHT('000000' +CAST(@numericPartBC AS VARCHAR(6)) ,6)
                    --Xử lý phần DML_Material_Label_Serial (đã tăng lên 1 giá trị)
                    SET @numericPartMLS = CAST(SUBSTRING(@MLSkey, 3, LEN(@MLSkey) - 2) AS INT)
                    SET @numericPartMLS = @numericPartMLS + 1
                    SET @MLSkey = 'ML' + RIGHT('000000000000000' + CAST(@numericPartMLS AS VARCHAR(13)), 13)
                    --Xử lý phần Data_Stock_In_Out (đã tăng lên 1 giá trị)
                    SET @numericPartSIOL = CAST(SUBSTRING(@SIOLkey, 3, LEN(@SIOLkey) - 2) AS INT)
                    SET @numericPartSIOL = @numericPartSIOL + 1
                    SET @SIOLkey = 'DS' + RIGHT('000000000000000' + CAST(@numericPartSIOL AS VARCHAR(13)), 13)

                        INSERT INTO [Data_Material_Label] (Material_Label_Serial, Supplier, Material_Name, Color, Size, QTY, Total_QTY, Print_QTY, Print_Times, 
                            Label_Status, Order_No, Roll, Production, Supplier_No, Material_No, Work_Order, Material_Type, BarCode, Modify_Date, Print_Date, User_Serial_Key, Arrival_QTY)  
                            SELECT @MLSkey , Supplier, Material_Name,  Color, Size, '{QTY}',Total_QTY, Print_QTY, Print_Times,
                            Label_Status, Order_No, Roll, Production, Supplier_No, Material_No, Work_Order, Material_Type, @BCkey, '{Modify_Date}',Print_Date,'{User_ID}', Arrival_QTY
                            FROM [Data_Material_Label]
                            WHERE BarCode = '{Barcode}';

                        INSERT INTO [Data_Stock_In_Out] 
                            (Stock_In_Out_Serial, Material_Label_Serial, Barcode, Stock_In_Out_Status, Stock_In_No 
                            ,Stock_Out_No, QTY, Print_Qty, Storage_Serial, Modify_Date, User_Serial_Key)
                            SELECT @SIOLkey, @MLSkey, @BCkey,'Out', Stock_In_No 
                            ,Stock_Out_No, '{QTY}', Print_Qty, '', '{Modify_Date}', '{User_ID}'
                            FROM [Data_Stock_In_Out] 
                            WHERE BarCode = '{Barcode}';
                    SELECT @BCkey AS NewBarcode
                            ";
                    DataTable results = connect.ExecuteQuery(insertOut);
                    if (results.Rows.Count > 0)
                    {
                        DataRow row = results.Rows[0];
                        LogServices lou = new LogServices();
                        //update số lượng 
                        int ghilog1 = lou.SetLogStockIO(Barcode, IP4_Address, "Update_Data_Stock_In_Out");
                        int ghilog3 = lou.SetLogMaterial(Barcode, IP4_Address, "Update_Data_Material_Label");

                        int ghilog2 = lou.SetLogStockIO(row["NewBarcode"].ToString(), IP4_Address, "Update_Data_Stock_In_Out");
                        int ghilog4 = lou.SetLogMaterial(row["NewBarcode"].ToString(), IP4_Address, "Save_Data_Material_Label");
                        return "Cập nhật thành công";

                    }
                    else
                    {
                        return "Cập nhật thất bại";
                    }

                }
                else
                {
                    return "Không tìm thấy vật tư hoặc đã xuất từ trước rồi ";
                }
            }
            catch (Exception ex)
            {
                return "Fail " + ex;

            }

        }


        // Trả vật tư về kho trong trường hợp xuất nhầm vật tư (xuất hết số lượng)
        public dynamic ReBackStockOut(string Barcode, string User_ID, string IP4_Address)
        {
            try
            {
                string querycheck = $"SELECT * FROM [Data_Stock_In_Out] WHERE [Barcode] ='{Barcode}' AND [Stock_In_Out_Status] = 'Out' ";
                DataTable resultcheck = connect.ExecuteQuery(querycheck);
                if (resultcheck.Rows.Count > 0)
                {
                    string format = "yyyy-MM-dd HH:mm:ss.fff";
                    DateTime currentDateTime = DateTime.Now;
                    DateTime Modify_Date = DateTime.ParseExact(currentDateTime.ToString(format), format, CultureInfo.InvariantCulture);

                    string updateso = $@"UPDATE [Data_Stock_In_Out] SET [Stock_In_Out_Status] = 'In',
                                [Storage_Serial] ='', [Modify_Date]='{Modify_Date}' , [User_Serial_Key]='{User_ID}' 
                                WHERE [BarCode] = '{Barcode}' 
                                SELECT SUM(QTY) AS SL
                            FROM Data_Stock_In_Out WHERE CAST(Modify_Date AS DATE) = CAST(GETDATE() AS DATE)  AND Stock_In_Out_Status = 'Out'
                              AND User_Serial_Key = '{User_ID}' GROUP BY User_Serial_Key";

                    DataTable result = connect.ExecuteQuery(updateso);


                    if (result.Rows.Count > 0)
                    {

                        DataRow row = result.Rows[0];
                        LogServices lou = new LogServices();
                        int ghilog = lou.SetLogStockIO(Barcode, IP4_Address, "Update_Return_Data_Stock_In " + Modify_Date);
                        return row["SL"].ToString();
                    }
                    else
                    {
                        return "Cập nhật thất bại";
                    }
                }
                else
                {
                    return "Không tìm thấy vật tư để trả về";
                }
            }
            catch (Exception ex)
            {
                return "Fail " + ex;
            }
        }

        //Tìm kiếm theo mã vật tư theo các khoảng thời gian 
        public dynamic GetMaterialNo(string Material_No, DateTime Date_Start, DateTime Date_End)
        {

            try
            {
                if (Date_Start.GetType() == typeof(DateTime) && Date_Start.GetType() == typeof(DateTime))
                {
                    string query = $@"SELECT sio.Stock_In_Out_Serial, sio.Material_Label_Serial, sio.Barcode, sio.Stock_In_Out_Status, sio.Stock_In_No,
                    sio.Stock_Out_No, sio.QTY AS SIO_QTY, sio.Print_Qty, sio.Storage_Serial, sio.Modify_Date AS SIO_Modify_Date,
                    sio.User_Serial_Key, dml.Material_Label_Serial, dml.Supplier, dml.Material_Name, dml.Color, dml.Size, dml.QTY AS DML_QTY,
                    dml.Total_QTY, dml.Print_QTY, dml.Print_Times, dml.Label_Status, dml.Order_No, dml.Roll, dml.Production,
                    dml.Supplier_No, dml.Material_No, dml.Work_Order, dml.Material_Type, dml.BarCode, dml.Modify_Date AS DML_Modify_Date,
                    dml.Print_Date, dml.User_Serial_Key, dml.Arrival_QTY
                FROM [Data_Stock_In_Out] sio
                INNER JOIN [Data_Material_Label] dml ON sio.Barcode = dml.BarCode
                WHERE dml.Material_No = '{Material_No}' AND sio.Stock_In_Out_Status = 'Out' AND sio.Modify_Date BETWEEN '{Date_Start}' AND '{Date_End}'";


                    DataTable results = connect.ExecuteQuery(query);
                    if (results.Rows.Count > 0)
                    {
                        List<Data_Material_Label> mangdml = new List<Data_Material_Label>();
                        //List<Data_Stock_In_Out> mangsio = new List<Data_Stock_In_Out>();

                        foreach (DataRow row in results.Rows)
                        {
                            //Data_Stock_In_Out sio = new Data_Stock_In_Out()
                            //{
                            //    Stock_In_Out_Serial = row["Stock_In_Out_Serial"].ToString(),
                            //    Material_Label_Serial = row["Material_Label_Serial"].ToString(),
                            //    Barcode = row["Barcode"].ToString(),
                            //    Stock_In_Out_Status = row["Stock_In_Out_Status"].ToString(),
                            //    Stock_In_No = row["Stock_In_No"].ToString(),
                            //    Stock_Out_No = row["Stock_Out_No"].ToString(),
                            //    QTY = double.Parse(row["SIO_QTY"].ToString()),
                            //    Print_Qty = row["Print_Qty"].ToString(),
                            //    Storage_Serial = row["Storage_Serial"].ToString(),
                            //    Modify_Date = DateTime.Parse(row["SIO_Modify_Date"].ToString()),
                            //    User_Serial_Key = row["User_Serial_Key"].ToString(),
                            //};

                            Data_Material_Label dml = new Data_Material_Label()
                            {
                                Material_Label_Serial = row["Material_Label_Serial"].ToString(),
                                Supplier = row["Supplier"].ToString(),
                                Material_Name = row["Material_Name"].ToString(),
                                Color = row["Color"].ToString(),
                                Size = row["Size"].ToString(),
                                QTY = double.Parse(row["DML_QTY"].ToString()),
                                Total_QTY = double.Parse(row["Total_QTY"].ToString()),
                                Print_QTY = row["Print_QTY"].ToString(),
                                Print_Times = int.Parse(row["Print_Times"].ToString()),
                                Label_Status = row["Label_Status"].ToString(),
                                Order_No = row["Order_No"].ToString(),
                                Roll = row["Roll"].ToString(),
                                Production = row["Production"].ToString(),
                                Supplier_No = row["Supplier_No"].ToString(),
                                Material_No = row["Material_No"].ToString(),
                                Work_Order = row["Work_Order"].ToString(),
                                Material_Type = row["Material_Type"].ToString(),
                                BarCode = row["BarCode"].ToString(),
                                Modify_Date = DateTime.Parse(row["DML_Modify_Date"].ToString()),
                                Print_Date = DateTime.Parse(row["Print_Date"].ToString()),
                                User_Serial_Key = row["User_Serial_Key"].ToString(),
                                Arrival_QTY = double.Parse(row["Arrival_QTY"].ToString())
                            };

                            mangdml.Add(dml);
                            //mangsio.Add(sio);
                        }

                        var response = new
                        {
                            dml = mangdml,
                            sio = ""
                        };

                        return response;
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                return "Fail " + ex;
            }
        }

        public dynamic getExportList(string Status, string Order_No, string Material_No, string Supplier, DateTime Date_Start, DateTime Date_End)
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
                        WHERE Stock_In_Out_Status = '{Status}' AND DSIO.Modify_Date BETWEEN '{Date_Start}' AND '{Date_End}'" + chuoisql + " ORDER BY Order_No, Material_No, DSIO.QTY";
                
                    DataTable result = connect.ExecuteQuery(query);
                    if (result.Rows.Count > 0)
                    {
                        List<ExportList> mang = new List<ExportList>();
                        string mavt = "";

                        ExportList temp = new ExportList();
                        foreach (DataRow row in result.Rows)
                        {
                            string[] roll = row["Roll"].ToString().Split('/');
                            string[] rolltemp = temp.Roll.ToString().Split('/');
                            string[] dv = row["PrintQTY"].ToString().Split(' ');

                            if (mavt == row["Material_No"].ToString())
                            {
                                temp.Order_No += row["Order_No"].ToString() + '-' + DateTime.Parse(row["Print_Date"].ToString()).ToString("dd/MM/yyyy") + " ";
                                temp.Roll = double.Parse(rolltemp[0].ToString()) + double.Parse(roll[0].ToString());
                                temp.QTY = double.Parse(temp.QTY.ToString()) + double.Parse(row["SL"].ToString());
                                temp.chitietkien += "+" + row["SL"].ToString();

                            }
                            else
                            {
                                if (mavt != "")
                                {
                                    string[] sophieu = temp.Order_No.Split(' ');
                                    var groupedSoPhieu = sophieu.GroupBy(x => x);
                                    temp.Order_No = string.Join(" ", groupedSoPhieu.Select(g => $"{g.Key}{(g.Count() > 1 ? " " : "")}"));

                                    string[] numbers = temp.chitietkien.Split('+');
                                    var groupedNumbers = numbers.GroupBy(x => x);
                                    temp.chitietkien = string.Join("+", groupedNumbers.Select(g => $"{g.Key}{(g.Count() > 1 ? "*" + g.Count().ToString() : "")}"));
                                    mang.Add(temp);
                                }
                                temp = new ExportList();
                                mavt = row["Material_No"].ToString();
                                temp.Rack = row["Rack"].ToString();
                                temp.Color = row["Color"].ToString();
                                temp.Size = row["Size"].ToString();
                                temp.Production = row["Production"].ToString();
                                temp.User_Serial_Key = row["UserKey"].ToString();
                                temp.Supplier = row["Supplier"].ToString();
                                temp.Supplier_No = row["Supplier_No"].ToString();
                                temp.Order_No = row["Order_No"].ToString() + '-' + DateTime.Parse(row["Print_Date"].ToString()).ToString("dd/MM/yyyy") + " ";
                                temp.Work_Order = row["Work_Order"].ToString();
                                temp.Material_No = row["Material_No"].ToString();
                                temp.Material_Name = row["Material_Name"].ToString();
                                temp.Roll = double.Parse(roll[0].ToString());
                                temp.QTY = double.Parse(row["SL"].ToString());
                                temp.chitietkien = row["SL"].ToString();
                                temp.Print_QTY = dv[1].ToString();
                                temp.Print_Date = DateTime.Parse(row["Print_Date"].ToString()).ToString("dd/MM/yyyy");
                            }

                        }

                        if (mavt != "")
                        {
                            string[] sophieu = temp.Order_No.Split(' ');
                            var groupedSoPhieu = sophieu.GroupBy(x => x);
                            temp.Order_No = string.Join(" ", groupedSoPhieu.Select(g => $"{g.Key}{(g.Count() > 1 ? " " : "")}"));
                            string[] numbers = temp.chitietkien.Split('+');
                            var groupedNumbers = numbers.GroupBy(x => x);
                            temp.chitietkien = string.Join("+", groupedNumbers.Select(g => $"{g.Key}{(g.Count() > 1 ? "*" + g.Count().ToString() : "")}"));
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
