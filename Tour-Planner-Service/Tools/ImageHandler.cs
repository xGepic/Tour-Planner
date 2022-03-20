using System.Drawing;
namespace Tour_Planner_Service;
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
internal static class ImageHandler
{
    private const int width = 400;
    private const int height = 300;
    private static byte[] Converter(Image imageToConvert)
    {
        ImageConverter myImageConverter = new();
        byte[] myByteArray = myImageConverter.ConvertTo(imageToConvert, typeof(byte[])) as byte[] ?? Array.Empty<byte>();
        return myByteArray;
    }
    public static byte[] ResizeImage(string path)
    {
        Image originalImage = Image.FromFile(path);
        var destinationRectangle = new Rectangle(0, 0, width, height);
        var destinationImage = new Bitmap(width, height);
        destinationImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);
        using (var graphics = Graphics.FromImage(destinationImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(originalImage, destinationRectangle, 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, wrapMode);
        }
        return Converter(destinationImage);
    }
}