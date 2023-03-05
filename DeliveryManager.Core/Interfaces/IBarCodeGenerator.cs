namespace DeliveryManager.Core.Interfaces
{
    public interface IBarCodeGenerator
    {
        byte[] GenerateCode39Barcode(string barcodeData, int barcodeImageWidth = 300, int barcodeImageHeight = 100);
    }
}