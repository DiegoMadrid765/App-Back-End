using Back_End.DTO;
using Back_End.IServices;
using Back_End.Models;
using Back_End.Utils;
using DinkToPdf;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("api/product")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService userService;
        private readonly IMailService mailService;




        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IUserService userService, IMailService mailService)
        {
            this.productService = productService;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
            this.mailService = mailService;

        }

        [HttpPost]
        [Route("RegisterProduct")]

        public async Task<IActionResult> RegisterProduct([FromForm] ProductDTO product)
        {
            if (product.image == null)
            {
                return BadRequest(new { error = "imagenull" });
            }
            if (product.image.Length > 5242880)
            {
                return BadRequest(new { error = "heavyimage" });
            }
            try
            {
                string directoryurl = Path.Combine(webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(directoryurl))
                {
                    Directory.CreateDirectory(directoryurl);
                }
                string filename = $"{Guid.NewGuid()}{Path.GetExtension(product.image.FileName)}";

                string route = Path.Combine(directoryurl, filename);
                using (var stream = new FileStream(route, FileMode.Create))
                {
                    product.image.CopyTo(stream);
                }
                var currentturl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
                var urldb = Path.Combine(currentturl, "images", filename).Replace("\\", "/");
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var productregister = new Product();
                productregister.imageurl = urldb;
                productregister.price = decimal.Parse(product.price);
                productregister.name = product.name;
                productregister.description = product.description;
                productregister.stock = product.stock;
                productregister.userId = JwtConfigurator.getTokenIdUser(identity);
                await productService.RegisterProduct(productregister);
                return Ok(new { mesagge = "registered" });
            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }


        }
        [HttpGet]
        [Route("Getproducts")]

        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userid = JwtConfigurator.getTokenIdUser(identity);
                return Ok(await productService.GetProducts(userid));
            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }
        }
        [HttpGet]
        [Route("GetMyProducts")]
        public async Task<IActionResult> GetMyProducts()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userid = JwtConfigurator.getTokenIdUser(identity);
                return Ok(await productService.GetMyProducts(userid));
            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }
        }

        [HttpPut]
        [Route("ShowOrHideProducts")]
        public async Task<IActionResult> ShowOrHideProducts(int idproduct)
        {
            try
            {
                var product = await productService.GetProductById(idproduct);
                if (product == null)
                {
                    return BadRequest(new { error = "error" });
                }
                if (product.hidden == 0)
                {
                    product.hidden = 1;
                }
                else
                {
                    product.hidden = 0;

                }
                await productService.SetHideProduct(product);
                return Ok(new { ok = "ok" });
            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }
        }
        [HttpPost]
        [Route("BuyProduct")]
        public async Task<IActionResult> BuyProduct(int idproduct)
        {
            try
            {
                var product = await productService.GetProductById(idproduct);
                if (product == null)
                {
                    return BadRequest(new { message = "error" });
                }
                var purchase = new Purchase();
                purchase.productId = idproduct;
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                purchase.userId = JwtConfigurator.getTokenIdUser(identity);
                purchase.DatePurchase = DateTime.Now;
                await productService.BuyProduct(purchase);
                return Ok(new { message = "regitared" });
            }
            catch (Exception)
            {

                return BadRequest(new { message = "error" });
            }


        }

        [HttpGet]
        [Route("GetPurchasesForPdf")]
        public async Task<IActionResult> GetPurchasesForPdf()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userid = JwtConfigurator.getTokenIdUser(identity);

                var user = await userService.GetUser(userid);
                var purchases = await productService.GetPurchases(userid);
                await mailService.SendEmailPurchases(purchases, user);
                return Ok(new { message = "emailsent" });
            }
            catch (Exception e)
            {

                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet]
        [Route("GetProductDetails")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userid = JwtConfigurator.getTokenIdUser(identity);

                var product = await productService.GetProductDetails(id, userid);
                if (product == null)
                {
                    return BadRequest(new { error = "error" });
                }
                else
                {
                    return Ok(product);
                }

            }
            catch (Exception e)
            {

                return BadRequest(new { error = e.Message });
            }
        }


        [HttpGet]
        [Route("SeeEditProduct")]
        public async Task<ActionResult<Product>> SeeEditProduct(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userid = JwtConfigurator.getTokenIdUser(identity);

                var product = await productService.SeeeEditProduct(id, userid);
                if (product == null)
                {
                    return BadRequest(new { title = "error", description = "An error has ocurred" });
                }
                else
                {
                    return product;
                }

            }
            catch (Exception e)
            {

                return BadRequest(new { error = e.Message });
            }
        }
        [HttpGet]
        [Route("DownloadPDFPurchases")]
        public async Task<IActionResult> DownloadPDFPurchases()
        {
            try
            {

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userid = JwtConfigurator.getTokenIdUser(identity);
                var user = await userService.GetUser(userid);
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
                    HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                
                var converter = new BasicConverter(new PdfTools());
               byte[] pdfBytes = converter.Convert(pdf);
               
                return File(pdfBytes, "application/pdf", $"data_{user.names}{new DateTime().Second}.pdf");
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }

        }


    }
}