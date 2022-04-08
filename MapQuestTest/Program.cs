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
        Uri endpoint = new(directionsUri, directionsParameters);
        HttpResponseMessage response = client.GetAsync(endpoint).Result;
        var result = JObject.Parse(response.Content.ReadAsStringAsync().Result);

        double distance = Convert.ToInt32(result.SelectToken("route.distance")) * toKM;
        TimeSpan time = TimeSpan.FromSeconds(Convert.ToInt32(result.SelectToken("route.time")));

        Console.WriteLine("Distance: " + distance + " km");
        Console.WriteLine("Time: " + time.ToString("hh':'mm"));
    }
    public static void CallStaticmapUri()
    {
        Uri endpoint = new(staticmapUri, staticmapParameters);
        byte[] myPic = client.GetByteArrayAsync(endpoint).Result;

        Image myImage = (Bitmap)new ImageConverter().ConvertFrom(myPic);
        string filePath = String.Concat(Directory.GetCurrentDirectory(), "test.jpg");

        myImage.Save(filePath);
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