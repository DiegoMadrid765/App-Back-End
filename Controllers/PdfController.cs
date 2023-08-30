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
        private readonly IProductService productService;

        public PdfController(IConverter converter, IUserService userService, IProductService productService)
        {
            this.converter = converter;
            this.userService = userService;
            this.productService = productService;
        }

        [HttpGet]
        [Route("DownloadPDF")]
        public async Task<IActionResult> DownloadPDF()
        {
            try
            {
                string htmlContent = System.IO.File.ReadAllText("Docs/Purchases.html");
                var currentdate = DateTime.Now;
                string date = $"{currentdate.Day}-{currentdate.Month}-{currentdate.Year}";
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userid = JwtConfigurator.getTokenIdUser(identity);
                var user = await userService.GetUser(userid);
                string names = $"{user.names} {user.lastnames}";
                string purchasescontent = string.Empty;

                var purchases = await productService.GetPurchases(userid);
                decimal total = 0;
                foreach (var purchase in purchases)
                {
                    purchasescontent += $"<tr><td>{purchase.product.name}</td>";
                    purchasescontent += $"<td>{purchase.product.price}</td>";
                    purchasescontent += $"<td>{purchase.DatePurchase.Day}-{purchase.DatePurchase.Month}-{purchase.DatePurchase.Year}</td>";
                    purchasescontent += $"<td>{purchase.user.names} {purchase.user.lastnames}</td></tr>";
                    total += purchase.product.price;
                }

                string finalhtmlcontent = htmlContent.Replace("{date}", date).Replace("{purchases}", purchasescontent).Replace("{names}", names).Replace("{total}", total.ToString());
                GlobalSettings globalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                };

                ObjectSettings objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = finalhtmlcontent,
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
