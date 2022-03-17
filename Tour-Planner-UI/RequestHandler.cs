namespace Tour_Planner_UI;

internal static class RequestHandler
{
    public static Tour[]? GetAllTours()
    {
        using HttpClient client = new();
        Uri endpoint = new("https://localhost:7122/Tour/GetAll");
        HttpResponseMessage Response = client.GetAsync(endpoint).Result;
        string ResultAsString = Response.Content.ReadAsStringAsync().Result;
        Tour[]? AllTours = JsonConvert.DeserializeObject<Tour[]>(ResultAsString);
        return AllTours;
    }
    //public static Tour? GetTourById()
    //{
    //    using HttpClient client = new();
    //    Uri endpoint = new("https://localhost:7122/Tour/GetByID");
    //    HttpResponseMessage Response = client.GetAsync(endpoint).Result;
    //    string ResultAsString = Response.Content.ReadAsStringAsync().Result;
    //    Tour? Tour = JsonConvert.DeserializeObject<Tour>(ResultAsString);
    //    return Tour;
    //}
    public static bool AddTour()
    {
        using HttpClient client = new();
        Uri endpoint = new("https://localhost:7122/Tour/AddTour");
        HttpResponseMessage Response = client.GetAsync(endpoint).Result;
        return Response.IsSuccessStatusCode;
    }
    public static bool UpdateTour()
    {
        using HttpClient client = new();
        Uri endpoint = new("https://localhost:7122/Tour/UpdateTour");
        HttpResponseMessage Response = client.GetAsync(endpoint).Result;
        return Response.IsSuccessStatusCode;
    }
    public static bool DeleteTour()
    {
        using HttpClient client = new();
        Uri endpoint = new("https://localhost:7122/Tour/DeleteTour");
        HttpResponseMessage Response = client.GetAsync(endpoint).Result;
        return Response.IsSuccessStatusCode;
    }
    public static bool SaveFile()
    {
        using HttpClient client = new();
        Uri endpoint = new("https://localhost:7122/Tour/SaveFile");
        HttpResponseMessage Response = client.GetAsync(endpoint).Result;
        return Response.IsSuccessStatusCode;
    }
}