using Back_End.Languajes;
using Back_End.Models;

namespace Back_End.Messages
{
    public class ProductMessages
    {
        Dictionary<string, Message> mesagges = new Dictionary<string, Message>();

        public ProductMessages()
        {
            mesagges.Add("hola", new Message(new SpanishMessage("", ""),new EnglishMessage("", "")));
        }
    }
   
}
