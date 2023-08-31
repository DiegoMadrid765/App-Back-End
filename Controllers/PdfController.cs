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

        



    }
}
