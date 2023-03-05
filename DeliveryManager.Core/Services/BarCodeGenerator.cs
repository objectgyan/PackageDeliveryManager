using System.Drawing;
using System.Drawing.Imaging;
using DeliveryManager.Core.Interfaces;

namespace DeliveryManager.Core.Services
{
    public class BarCodeGenerator : IBarCodeGenerator
    {
        public byte[] GenerateCode39Barcode(string barcodeData, int barcodeImageWidth = 300, int barcodeImageHeight = 100)
        {
            // Define the characters and bar patterns for Code 39
            string[] code39Characters = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "-", ".", " ", "$", "/", "+", "%", "*" };
            string[] code39BarPatterns = { "111221211", "211211112", "112211112", "212211111", "111221112", "211221111", "112221111", "111211212", "211211211", "112211211", "211112112", "112112112", "212112111", "111122112", "211122111", "112122111", "111112212", "211112211", "112112211", "111122211", "211111122", "112111122", "212111121", "111121122", "211121121", "112121121", "111111222", "211111221", "112111221", "111121221", "221111112", "122111112", "222111111", "121121112", "221121111", "122121111", "121111212", "221111211", "122111211", "121121211", "121212111", "121211121", "121112121", "111212121", "121121212", "121212112", "112121212", "111222111" };

            // Calculate the checksum character
            int checksum = 0;
            for (int i = 0; i < barcodeData.Length; i++)
            {
                string character = barcodeData[i].ToString().ToUpper();
                int characterIndex = Array.IndexOf(code39Characters, character);
                if (characterIndex == -1)
                {
                    throw new ArgumentException("Invalid character in barcode data.");
                }
                checksum += characterIndex;
            }
            int checksumIndex = checksum % 43;
            string checksumCharacter = code39Characters[checksumIndex];

            // Add the checksum character to the barcode data
            barcodeData += checksumCharacter;

            // Create a new bitmap to draw the barcode image
            Bitmap barcodeImage = new Bitmap(barcodeImageWidth, barcodeImageHeight + 20);

            using (Graphics g = Graphics.FromImage(barcodeImage))
            {
                // Clear the image and set the background color
                g.Clear(Color.White);

                // Calculate the bar width
                float barWidth = (float)barcodeImageWidth / ((barcodeData.Length + 2) * 12 + 13);

                // Draw the barcode
                float x = 0;
                for (int i = 0; i < barcodeData.Length; i++)
                {
                    string character = barcodeData[i].ToString().ToUpper();
                    int characterIndex = Array.IndexOf(code39Characters, character);
                    if (characterIndex == -1)
                    {
                        throw new ArgumentException("Invalid character in barcode data.");
                    }
                    string barPattern = code39BarPatterns[characterIndex];
                    // Draw the bars for the character
                    for (int j = 0; j < barPattern.Length; j++)
                    {
                        float barHeight = j % 2 == 0 ? barcodeImageHeight : barcodeImageHeight / 2;
                        if (barPattern[j] == '1')
                        {
                            g.FillRectangle(Brushes.Black, x, 0, barWidth, barHeight);
                        }
                        x += barWidth;
                    }

                    // Add a space between characters
                    x += barWidth;
                }

                // Add the start and end characters to the barcode
                string startBarPattern = code39BarPatterns[39];
                for (int i = 0; i < startBarPattern.Length; i++)
                {
                    float barHeight = i % 2 == 0 ? barcodeImageHeight : barcodeImageHeight / 2;
                    if (startBarPattern[i] == '1')
                    {
                        g.FillRectangle(Brushes.Black, x, 0, barWidth, barHeight);
                    }
                    x += barWidth;
                }
                x += barWidth;
                string endBarPattern = code39BarPatterns[checksumIndex];
                for (int i = 0; i < endBarPattern.Length; i++)
                {
                    float barHeight = i % 2 == 0 ? barcodeImageHeight : barcodeImageHeight / 2;
                    if (endBarPattern[i] == '1')
                    {
                        g.FillRectangle(Brushes.Black, x, 0, barWidth, barHeight);
                    }
                    x += barWidth;
                }

                // Draw the barcode data as text below the barcode
                g.DrawString(barcodeData, new Font("Arial", 10), Brushes.Black, 0, barcodeImageHeight + 5);
            }
            // Return the barcode image as a FileResult
            MemoryStream ms = new MemoryStream();
            barcodeImage.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
