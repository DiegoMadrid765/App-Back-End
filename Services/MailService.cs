using Back_End.IServices;
using Back_End.Models;
using System.Net;
using System.Net.Mail;
namespace Back_End.Utils
{
    public class MailService:IMailService
    {
        readonly static private string emailfrom = "ecommercec765@gmail.com";
        readonly static private string password = "xyghtytxemyafotc";
        readonly static private string websitefrontend = "localhost:4200";
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        public MailService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }
        public async Task SendEmailAutorization(User user, string url)
        {
            
            
            
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailfrom, password)


                };
              
                var email = new MailMessage(emailfrom, user.email);
               
                email.Subject = "Activate your account";
                email.Body = $"<p>Hello {user.names} {user.lastnames}! thank you for creating an account with us, now the next step is activating your account. <br>  Please enter to this link to activate it: </p> <a href='http://{websitefrontend}/welcome/activate/{url}'>Activate account</a>";
                email.IsBodyHtml = true;
               
                  await  client.SendMailAsync(email);
                

                
            }
            catch (Exception)
            {

              

            }
        }

        public async Task SendEmailPurchases(List<Purchase> PurchaseList,User user)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailfrom, password)


                };
               
                var htmlToPdf = new HtmlToPdf();
                var currentdate = DateTime.Now;
                var tablecontent = string.Empty;
                decimal total = 0;
                string userhtml = $"{user.names} {user.lastnames}";
                string datehtml= $"{currentdate.Day}/{currentdate.Month}/{currentdate.Year} {currentdate.Hour}:{currentdate.Minute}:{currentdate.Second}";
                foreach (var purchase in PurchaseList)
                {
                    tablecontent+=($"<tr><td>{purchase.product.name}</td><td>{purchase.product.price}</td><td>{purchase.DatePurchase.Day}/{purchase.DatePurchase.Month}/{purchase.DatePurchase.Year} {purchase.DatePurchase.Hour}:{purchase.DatePurchase.Minute}:{purchase.DatePurchase.Second}</td><td>{purchase.product.user.names} {purchase.product.user.lastnames}</td><tr>");
                    total+= purchase.product.price;
                }
                var htmlfile = "<!DOCTYPE html><html lang=\"en\"><head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> body { font-family: Arial, Helvetica, sans-serif; } table { font-family: \"Lucida Sans Unicode\", \"Lucida Grande\", Sans-Serif; font-size: 20px; text-align: left; border-collapse: collapse; width: 100%; } th { font-size: 20px; font-weight: normal; padding: 8px; background: #b9c9fe; border-top: 4px solid #aabcfe; border-bottom: 1px solid #fff; color: #039; } td { padding: 8px; background: #e8edff; border-bottom: 1px solid #fff; color: #669; border-top: 1px solid transparent; } </style></head><body> <h2>Hi "+userhtml+"! on this report you will see all the purchases you have made.</h2> <h3>Date:"+ datehtml +"</h3> <table> <tr> <th>Product Name</th> <th>Product Price</th> <th>Date</th> <th>Product Seller</th> </tr> <tr> "+tablecontent+" </tr> <tr> <th>Total</th> <td> "+total+" </td> </tr> </table></body></html>";

                PdfDocument pdf = await htmlToPdf.RenderHtmlAsPdfAsync(htmlfile);

                string directoryurl = Path.Combine(webHostEnvironment.WebRootPath, "pdfs");
                if (!Directory.Exists(directoryurl))
                {
                    Directory.CreateDirectory(directoryurl);
                }
                var filename = Path.Combine(directoryurl, $"purchases {user.names} {user.lastnames}.pdf");
                pdf.SaveAs(filename);
                var attachment = new Attachment(filename);
                var email = new MailMessage(emailfrom, user.email);
                email.Attachments.Add(attachment);
                email.Subject = "Purchases";
                email.Body = $"<p>Hello {user.names} {user.lastnames}! here you go your list of purchases you have made so far.";
                email.IsBodyHtml = true;
                await client.SendMailAsync(email);
                
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
               
            }
            catch (Exception)
            {
            }
        }
        public async Task SendEmailForgotAutorization(User user, string url)
        {



            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailfrom, password)


                };
                var email = new MailMessage(emailfrom, user.email);
                email.Subject = "Activate your account";
                email.Body = $"<p>Hello {user.names} {user.lastnames}! As you forgot your credentials to activate your account, we are going to send it again. <br>  Please enter to this link to activate it: </p> <a href='http://{websitefrontend}/welcome/activate/{url}'>Acivate account</a>";
                email.IsBodyHtml = true;

               await client.SendMailAsync(email);



            }
            catch (Exception)
            {



            }
        }
    }
}
