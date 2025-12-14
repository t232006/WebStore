using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace WebStore.Controllers
{
    public class QRController: Controller
    {
        public IActionResult Code(string code)
        {
            var Generator = new QRCodeGenerator();
            var Data = Generator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            var QRCode = new PngByteQRCode(Data);
            var image_bytes = QRCode.GetGraphic(20);
            return File(image_bytes, "image/png");
        } 
        
    }
}
