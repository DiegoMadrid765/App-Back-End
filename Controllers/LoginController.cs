using Back_End.DTO;
using Back_End.IServices;
using Back_End.Models;
using Back_End.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("api/Login")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly IMailService mailService;

        public LoginController(ILoginService loginService, IConfiguration configuration, IUserService userService ,IMailService mailService)
        {
            this.loginService = loginService;
            this.configuration = configuration;
            this.userService = userService;
            this.mailService = mailService;
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login(Login login)
        {


            var user = await loginService.GetUserbyEmail(login.email);
            if (user == null)
            {
                return BadRequest(new { message = "User doesnt exist" });
            }
            else
            {
                if (BCrypt.Net.BCrypt.Verify(login.password, user.password))
                {
                    if (!await userService.VerifyUserActivated(user.Id))
                    {
                        return BadRequest(new { message = "notActivated" });
                    }
                    else
                    {
                        string token = JwtConfigurator.GetToken(user, configuration);
                        return Ok(new { token = token });
                    }

                }
                else
                {
                    return BadRequest(new { message = "Password incorrect" });
                }
            }
        }

        [HttpGet]
        [Route("checkEmail")]
        public async Task<IActionResult> checkEmail(string email)
        {
            if (await loginService.GetUserbyEmail(email) == null)
            {
                return Ok(new { message = "yes" });
            }
            else
            {
                return BadRequest(new { message = "no" });
            }
        }
        [HttpPost]
        [Route("SendEmailResetActivateAccount")]
        public async Task<IActionResult> SendEmailResetActivateAccount(string email)
        {
            try
            {
                var user = await loginService.GetUserbyEmail(email);
                if (user != null)
                {
                    await userService.DeleteActivatedAccount(user.Id);
                    var activateduser = new ActivatedUser();
                    activateduser.activated = 0;
                    activateduser.userId = user.Id;
                    activateduser.url = Guid .NewGuid().ToString();
                   await mailService.SendEmailForgotAutorization(user,activateduser.url);
                    await userService.AddActivatedAccount(activateduser);
                    return Ok(new { messaje = "mail sent" });
                }  
                {
                    return BadRequest(new {error="error" });
                }
            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }
        }
    }
}
