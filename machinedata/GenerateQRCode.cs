using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Windows.Forms;

namespace machinedata
{
    internal class GenerateQRCode
    {
        public static Bitmap GenerateAndDisplayQRCode(string qrText)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return qrCodeImage;

            //pictureBox1.Image = qrCodeImage;
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
