﻿namespace Tour_Planner_UI.Repositories
{
    internal class MapRepository
    {
        private static readonly HttpClient client = new();

        private static readonly Uri directionsUri = new("http://www.mapquestapi.com/directions/v2/");
        private static string directionsParameters = "route?key=yKBh4sWxDYGp5iebnTtjXT4YKHR3KXnE&from=Wien&to=Berlin";
        private static readonly double toKM = 1.61;

        private static readonly Uri staticmapUri = new("http://www.mapquestapi.com/staticmap/v5/");
        private static readonly string staticmapParameters = "map?key=yKBh4sWxDYGp5iebnTtjXT4YKHR3KXnE&sizie=800,600&start=Wien&end=Berlin&defaultMarker=none";

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
        public static void CallStaticmapUri()
        {
            try
            {
                Uri endpoint = new(staticmapUri, staticmapParameters);
                byte[] myPic = client.GetByteArrayAsync(endpoint).Result;

                System.Drawing.Image myImage = (Bitmap)new ImageConverter().ConvertFrom(myPic);
                string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Images" + "/test.jpg";

                myImage.Save(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
