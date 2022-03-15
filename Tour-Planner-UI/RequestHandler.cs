namespace Tour_Planner_UI;

internal class RequestHandler
{
    public static TourLog[]? GetAllTourLogs()
    {
        using HttpClient client = new();
        Uri endpoint = new("https://localhost:7122/TourLog/GetAll");
        HttpResponseMessage Response = client.GetAsync(endpoint).Result;
        string ResultAsString = Response.Content.ReadAsStringAsync().Result;
        TourLog[]? AllTourLogs = JsonConvert.DeserializeObject<TourLog[]>(ResultAsString);
        return AllTourLogs;
    }
    public static Tour[]? GetAllTours()
    {
        using HttpClient client = new();
        Uri endpoint = new("https://localhost:7122/Tour/GetAll");
        HttpResponseMessage Response = client.GetAsync(endpoint).Result;
        string ResultAsString = Response.Content.ReadAsStringAsync().Result;
        Tour[]? AllTours = JsonConvert.DeserializeObject<Tour[]>(ResultAsString);
        return AllTours;
    }
}
