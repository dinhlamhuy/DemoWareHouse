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
    public class StockInController : ApiController
    {
        [Route("api/listStockInByRack/{Rack}")]
        [HttpGet]
        public IHttpActionResult ListStockInByRack(string Rack)
        {
            try
            {
                StockInService Si = new StockInService();
                dynamic result = Si.getMaterialByRack(Rack);
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


        [Route("api/OutRack/")]
        [HttpPost]
        public IHttpActionResult setOutRack(StockOutRequest req)
        {
            try
            {
                StockInService sp = new StockInService();
                dynamic result = sp.OutRack(req.Barcode, req.User_ID, req.IP4_Address);
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

        [Route("api/setMaterialRack/")]
        [HttpPost]
        public IHttpActionResult setMaterialRack(StockOutRequest req)
        {
            try
            {
                StockInService sp = new StockInService();
                dynamic result = sp.SetMaterialForRack(req.Barcode, req.Rack ,req.User_ID, req.IP4_Address);
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


        [Route("api/getExportInList")]
        [HttpPost]
        public IHttpActionResult GetExportInList(GetExportListRequest req)
        {
            try
            {
                StockInService sp = new StockInService();
                dynamic result = sp.getExportInList("In", req.Order_No, req.Material_No, req.Supplier, req.Date_Start, req.Date_End);
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
