using QRCoder;

namespace Back_End.Utils
{
    public class QRHelper
    {
        public string GenerateQR(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            var qrgenerator = new QRCodeGenerator();
            var qrcodedata = qrgenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode bitmapBycode = new BitmapByteQRCode(qrcodedata);
            var bitMap = bitmapBycode.GetGraphic(20);
            using var ms = new MemoryStream();
            ms.Write(bitMap);
            byte[] byteImage = ms.ToArray();
            return $"data:image/png;base64,{Convert.ToBase64String(byteImage)}";
        }
    }
}
