using Back_End.IServices;
using Back_End.Models;
using System.Net;
using System.Net.Mail;
using static QRCoder.PayloadGenerator;
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

        public async Task<bool> SendEmailForgotPassword(User user, string url)
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
                email.Subject = "Recovery password";
                email.Body = $"Hello {user.names} {user.lastnames}! click on this link to reset your password <a href='http://{websitefrontend}/welcome/reset-password/{url}'>Reset password</a>";
                email.IsBodyHtml = true;

                await client.SendMailAsync(email);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
