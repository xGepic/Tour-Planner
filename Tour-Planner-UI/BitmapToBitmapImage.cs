namespace Tour_Planner_UI
{
    internal class BitmapToBitmapImage
    {
        public static BitmapImage ConvertToBitmapImage(Bitmap src)
        {
            MemoryStream ms = new();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
