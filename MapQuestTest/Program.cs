using Newtonsoft.Json.Linq;
using System.Drawing;

namespace MapQuestTest;
public static class MapQuestTest
{
    private static readonly HttpClient client = new();

    private static readonly Uri directionsUri = new("http://www.mapquestapi.com/directions/v2/");
    private static readonly string directionsParameters = "route?key=yKBh4sWxDYGp5iebnTtjXT4YKHR3KXnE&from=Wien&to=Berlin";
    private static readonly double toKM = 1.61;

    private static readonly Uri staticmapUri = new("http://www.mapquestapi.com/staticmap/v5/");
    private static readonly string staticmapParameters = "map?key=yKBh4sWxDYGp5iebnTtjXT4YKHR3KXnE&sizie=800,600&start=Wien&end=Berlin&defaultMarker=none";

    public static void CallDirectionsUri()
    {
        try
        {
            Uri endpoint = new(directionsUri, directionsParameters);
            HttpResponseMessage response = client.GetAsync(endpoint).Result;
            JObject result = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            double distance = Convert.ToInt32(result.SelectToken("route.distance")) * toKM;
            TimeSpan time = TimeSpan.FromSeconds(Convert.ToInt32(result.SelectToken("route.time")));

            Console.WriteLine("Distance: " + distance + " km");
            Console.WriteLine("Time: " + time.ToString("hh':'mm"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
    public static void CallStaticmapUri()
    {
        try
        {
            Uri endpoint = new(staticmapUri, staticmapParameters);
            byte[] myPic = client.GetByteArrayAsync(endpoint).Result;

            Image myImage = (Bitmap)new ImageConverter().ConvertFrom(myPic);
            string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Images" + "/test.jpg";

            myImage.Save(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
}
class Program
{
    static void Main()
    {
        MapQuestTest.CallDirectionsUri();
        MapQuestTest.CallStaticmapUri();
    }
}