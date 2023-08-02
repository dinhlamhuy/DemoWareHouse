using DemoWareHouse.Models;
using DemoWareHouse.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWareHouse.Controllers
{
    public class StockOutController : ApiController
    {

        [Route("api/listStockInOut/{BarCode}")]
        [HttpGet]
        public IHttpActionResult ListStockInOut(string BarCode)
        {
            try
            {
                StockOutServices SOI = new StockOutServices();
                List<Data_Stock_In_Out> result = SOI.GetListStockIO(BarCode);
                var response = new
                {
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalServerError(Ex);
            }
        }

        [Route("api/ListMaterialLabel/{BarCode}")]
        [HttpGet]
        public IHttpActionResult ListMaterialLabel(string BarCode)
        {
            try
            {
                StockOutServices MLabel = new StockOutServices();
                List<Data_Material_Label> result = MLabel.GetListMaterialLabel(BarCode);
                var response = new
                {
                    Data = result,
                };
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalServerError(Ex);
            }
        }

        [Route("api/getMaterial/{BarCode}")]
        [HttpGet]
        public IHttpActionResult GetgetMaterial(string BarCode)
        {
            try
            {
                StockOutServices sp = new StockOutServices();
                dynamic result = sp.GetStockIO(BarCode);
                var response = new
                {
                    Data = result,
                };
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalServerError(Ex);
            }
        }

        [Route("api/stockoutall/")]
        [HttpPost]
        public IHttpActionResult StockOutALL(StockOutRequest req)
        {
            try
            {
                StockOutServices sp = new StockOutServices();
                dynamic result = sp.StockOutALL(req.Barcode, req.User_ID, req.IP4_Address);
                var response = new
                {
                    Data = result,
                };
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalServerError(Ex);
            }
        }

        [Route("api/stockout/")]
        [HttpPost]
        public IHttpActionResult StockOut(StockOutRequest req)
        {
            try
            {
                StockOutServices sp = new StockOutServices();
                dynamic result = sp.StockOut(req.Barcode, req.Tong_QTY.ToString(), req.QTY.ToString(), req.User_ID, req.IP4_Address);
                var response = new
                {
                    Data = result,
                };
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalServerError(Ex);
            }
        }


        [Route("api/rebackstockoutall/")]
        [HttpPost]
        public IHttpActionResult ReBackStockOutAll(StockOutRequest req)
        {
            try
            {
                StockOutServices sp = new StockOutServices();
                dynamic result = sp.ReBackStockOut(req.Barcode, req.User_ID, req.IP4_Address);
                var response = new
                {
                    Data = result,
                };
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalServerError(Ex);
            }
        }

        [Route("api/getMaterialNo/")]
        [HttpPost]
        public IHttpActionResult GetMaterialNo(GetMaterialNoRequest req)
        {
            try
            {
                StockOutServices sp = new StockOutServices();
                dynamic result = sp.GetMaterialNo(req.Material_No, req.Date_Start, req.Date_End);
                var response = new
                {
                    Data = result,
                };
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalServerError(Ex);
            }
        }

        [Route("api/getExportList")]
        [HttpPost]
        public IHttpActionResult GetExportList(GetExportListRequest req)
        {
            try
            {
                StockOutServices sp = new StockOutServices();
                dynamic result = sp.getExportList("Out",req.Order_No,req.Material_No, req.Supplier, req.Date_Start, req.Date_End);
                var response = new
                {
                    Data = result,
                };
                return Ok(response);
            }
            catch (Exception Ex)
            {
                return InternalServerError(Ex);
            }
        }

    }
}
