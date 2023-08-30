using Back_End.IServices;
using Back_End.Utils;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Back_End.Controllers

{
    [ApiController]
    [Route("api/Pdf")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PdfController : ControllerBase
    {
        private readonly IConverter converter;
        private readonly IUserService userService;

        public PdfController(IConverter converter,IUserService userService)
        {
            this.converter = converter;
            this.userService = userService;
        }

        [HttpGet]
        [Route("DownloadPDF")]
        public async Task<IActionResult> DownloadPDF()
        {
            try
            {

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userid = JwtConfigurator.getTokenIdUser(identity);
                var user= await userService.GetUser(userid);   
                string htmlContent = $"<h1>Hello, {user.names} {user.lastnames} this is HTML content in PDF!</h1>";
                GlobalSettings globalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                };

                ObjectSettings objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontSize = 9, Right = "Página [page] de [toPage]", Line = true, Spacing = 2.812 }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

               
                byte[] pdfBytes = converter.Convert(pdf);

                return File(pdfBytes, "application/pdf", $"data_{new DateTime().Second}.pdf");
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }

        }



    }
}
