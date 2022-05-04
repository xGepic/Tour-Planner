namespace Tour_Planner_UI.Repositories
{
    internal class MapRepository
    {
        private static readonly HttpClient client = new();

        private static readonly Uri directionsUri = new("http://www.mapquestapi.com/directions/v2/");
        private static string directionsParameters = string.Empty;
        private static readonly double toKM = 1.61;

        private static readonly Uri staticmapUri = new("http://www.mapquestapi.com/staticmap/v5/");
        private static string staticmapParameters = string.Empty;

        public static Tuple<double?, uint?> CallDirectionsUri(string start, string destination)
        {
            try
            {
                directionsParameters = $"route?key=yKBh4sWxDYGp5iebnTtjXT4YKHR3KXnE&from={start}&to={destination}";
                Uri endpoint = new(directionsUri, directionsParameters);
                HttpResponseMessage response = client.GetAsync(endpoint).Result;
                JObject result = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                double distance = Convert.ToInt32(result.SelectToken("route.distance")) * toKM;
                TimeSpan time = TimeSpan.FromSeconds(Convert.ToInt32(result.SelectToken("route.time")));
                uint timeAsUint = Convert.ToUInt32(time.TotalMinutes);

                if(distance <= 0 || timeAsUint <= 0)
                {
                    return new Tuple<double?, uint?>(null, null);
                }
                else
                {
                    return new Tuple<double?, uint?>(distance, timeAsUint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Tuple<double?, uint?>(null, null);
            }

        }
        public static BitmapImage CallStaticmapUri(string start, string destination)
        {
            try
            {
                staticmapParameters = $"map?key=yKBh4sWxDYGp5iebnTtjXT4YKHR3KXnE&sizie=800,600&start={start}&end={destination}&defaultMarker=none";
                Uri endpoint = new(staticmapUri, staticmapParameters);
                byte[] myPic = client.GetByteArrayAsync(endpoint).Result;
                Bitmap bitmap = (Bitmap)new ImageConverter().ConvertFrom(myPic);
                return ConvertToBitmapImage(bitmap);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        private static BitmapImage ConvertToBitmapImage(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
