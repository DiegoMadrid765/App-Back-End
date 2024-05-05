using Back_End.Languajes;

namespace Back_End.Models
{
    public class Message
    {
        public Message(SpanishMessage spanishMessage, EnglishMessage englishMessage)
        {
            this.spanishMessage = spanishMessage;
            this.englishMessage = englishMessage;
        }

        public SpanishMessage spanishMessage { get; set; }
        public EnglishMessage englishMessage { get; set; }
    }
}
