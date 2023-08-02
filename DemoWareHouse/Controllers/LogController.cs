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
    public class LogController : ApiController
    {

        [Route("api/LogUser")]
        [HttpPost]
        public IHttpActionResult LogUser(LogUserRequest req)
        {
            LogServices luser = new LogServices();
            int  result = luser.SetLogUser(req.User_ID, req.IP4_Address, req.Program_Log);


            var response = new
            {
                Data= result
            };
            return Ok(response);
        }

        [Route("api/LogMaterial")]
        [HttpPost]
        public IHttpActionResult LogMaterial(LogMaterialLabelRequest req)
        {
            LogServices luser = new LogServices();
            int result = luser.SetLogMaterial(req.BarCode, req.IP4_Address, req.Program_Log);


            var response = new
            {
                Data = result
            };
            return Ok(response);
        }

        [Route("api/LogStockOut")]
        [HttpPost]
        public IHttpActionResult LogStockOut(LogStockOutRequest req)
        {
            LogServices lou = new LogServices();  
            int result = lou.SetLogStockIO(req.Barcode,req.IP4_Address, req.Program_Log);
            var response = new
            {
                Data = result
            };
            return Ok(response);
        }
    }
}
