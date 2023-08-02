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
    public class HomeController : ApiController
    {
        [Route("api/getUser")]
        [HttpGet]
        public IHttpActionResult getDataUser()
        {
            UserServices getUser = new UserServices();
            var response = new
            {
                Message = "Thành công",
                Data = getUser.ListUser()
            };
            return Ok(response);
        }
        [Route("api/getDGroup")]
        [HttpGet]
        public IHttpActionResult getDataGroup()
        {
            UserServices getDGroup = new UserServices();
            var response = new
            {
                Message = "Thành công",
                Data = getDGroup.ListDGroup()
            };
            return Ok(response);
        }

        [Route("api/getLanguage/{lang}")]
        [HttpGet]
        public IHttpActionResult getLanguage(string lang)
        {
            UserServices getLang = new UserServices();
            var response = new
            {
                Message = "Thành công",
                Data = getLang.ListLanguage(lang)
            };
            return Ok(response);
        }


        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody] LoginRequest req)
        {
            AuthServices userlogin = new AuthServices();
            dynamic result = userlogin.LoginService(req.User_ID, req.User_Password);

            if (result is Data_User)
            {
                var response = new
                {
                    Message = 1,
                    Data = result
                };
                return Ok(response);
            }
            else
            {
                var response = new
                {
                    Message = result

                };
                return Ok(response);
            }
        }

        [Route("api/register")]
        [HttpPost]
        public IHttpActionResult Register(RegisterRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AuthServices registration = new AuthServices();
            var result = registration.RegisterService(req);
            var response = new
            {
                Message = result
            };
            return Ok(response);

        }

        [Route("api/changepassword")]
        [HttpPost]
        public IHttpActionResult ChangePassword([FromBody] ChangePasswordRequestModel req)
        {
            AuthServices authService = new AuthServices();
            int isPasswordChanged = authService.ChangePassword(req.User_ID, req.OldPassword, req.NewPassword);
            string Message;
            if (isPasswordChanged == 0)
            {
                Message = "Thành công";
            }
            else if (isPasswordChanged == 2)
            {
                Message = "Mật khẩu cũ không đúng";
            }
            else
            {
                Message = "Không tìm thấy tài khoản";
            }
            var response = new
            {
                Message = Message,
                Status= isPasswordChanged
            };
            return Ok(response);
        }

        [Route("api/changeLang")]
        [HttpPost]
        public IHttpActionResult ChangeLanguage([FromBody] LanguageRequest req)
        {
            UserServices lang = new UserServices();
            dynamic result = lang.ChangeLang(req.User_ID, req.TLLanguage);
            var response = new
            {
                Message = result,
            };
            return Ok(response);

        }
    }
}
