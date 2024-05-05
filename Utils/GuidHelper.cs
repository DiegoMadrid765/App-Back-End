namespace Back_End.Utils
{
    public class GuidHelper
    {
        public  string GetRandomText()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
