using BookStoreBLinterface;
using BookStoreCommonLayer.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBl;
        public UserController(IUserBL userBl)
        {
            this.userBl = userBl;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult AddUser(Users user)
        {
            try
            {
                this.userBl.AddUser(user);
                return this.Ok(new { success = true, message = "Registration Successful " });
            }

            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult LoginUser(UserLoginModel UserloginModel)
        {
            try
            {
                var token = this.userBl.Login(UserloginModel.EmailId, UserloginModel.Password);
                if (token == null)
                    return Unauthorized();
                return this.Ok(new { token = token, success = true, message = "Token Generated Successfull" });

            }
            catch (Exception e)
            {


                return this.BadRequest(new { success = false, message = e.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword(Users user)
        {
            try
            {
                bool isExist = this.userBl.ForgotPassword(user.EmailId);
                if (isExist) return Ok(new { success = true, message = $"Reset Link sent to {user.EmailId}" });
                else return BadRequest(new { success = false, message = $"No user Exist with {user.EmailId}" });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}



