using Newtonsoft.Json.Linq;

namespace MapQuestTest;
public static class MapQuestTest
{
    private static readonly HttpClient client = new();
    private static readonly Uri baseUri = new("http://www.mapquestapi.com/directions/v2/");
    private static readonly string parameters = "route?key=yKBh4sWxDYGp5iebnTtjXT4YKHR3KXnE&from=Wien&to=Berlin";
    public static void CallUri()
    {
        Uri endpoint = new(baseUri, parameters);
        HttpResponseMessage Response = client.GetAsync(endpoint).Result;
        var result = JObject.Parse(Response.Content.ReadAsStringAsync().Result);

        //distance
        double distance = Convert.ToInt32(result.SelectToken("route.distance")) * 1.61;

        //time
        TimeSpan time = TimeSpan.FromSeconds(Convert.ToInt32(result.SelectToken("route.time")));

        Console.WriteLine("Distance: " + distance + " km");
        Console.WriteLine("Time: " + time.ToString("hh':'mm"));
    }
}
class Program
{
    static void Main()
    {
        MapQuestTest.CallUri();
    }
}