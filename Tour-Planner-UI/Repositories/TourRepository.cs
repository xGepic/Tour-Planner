namespace Tour_Planner_UI;
internal static class TourRepository
{
    private static readonly HttpClient Client = new();
    private static readonly Uri BaseUri = new("https://localhost:7122/Tour/");
    public static Tour[]? GetAllTours()
    {
        Uri endpoint = new(BaseUri, "GetAll");
        HttpResponseMessage response = Client.GetAsync(endpoint).Result;
        string resultAsString = response.Content.ReadAsStringAsync().Result;
        Tour[]? allTours = JsonConvert.DeserializeObject<Tour[]>(resultAsString);
        return allTours;
    }
    public static Tour? GetTourById(Guid id)
    {
        string route = "GetByID?id=" + id.ToString();
        Uri endpoint = new(BaseUri, route);
        HttpResponseMessage response = Client.GetAsync(endpoint).Result;
        string resultAsString = response.Content.ReadAsStringAsync().Result;
        Tour? tour = JsonConvert.DeserializeObject<Tour>(resultAsString);
        return tour;
    }
    public static bool AddTour(string tourName, string tourDescription, string tourStartingPoint, string tourDestination, TransportType tourTransportType, TourType tourTourType, double tourDistance, uint tourEstimatedTimeInMin)
    {
        Uri endpoint = new(BaseUri, "AddTour");
        TourDTO tourToAdd = new()
        {
            TourName = tourName,
            TourDescription = tourDescription,
            StartingPoint = tourStartingPoint,
            Destination = tourDestination,
            TransportType = tourTransportType,
            TourDistance = tourDistance,
            EstimatedTimeInMin = tourEstimatedTimeInMin,
            TourType = tourTourType,
        };
        string tourToAddJson = JsonConvert.SerializeObject(tourToAdd);
        StringContent payload = new(tourToAddJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = Client.PostAsync(endpoint, payload).Result;
        return response.IsSuccessStatusCode;
    }
    public static bool UpdateTour(Guid tourId, string tourName, string tourDescription, string tourStartingPoint, string tourDestination, TransportType tourTransportType, TourType tourTourType, double tourDistance, uint tourEstimatedTimeInMin)
    {
        Uri endpoint = new(BaseUri, "UpdateTour");
        TourDTO tourToModify = new()
        {
            Id = tourId,
            TourName = tourName,
            TourDescription = tourDescription,
            StartingPoint = tourStartingPoint,
            Destination = tourDestination,
            TransportType = tourTransportType,
            TourDistance = tourDistance,
            EstimatedTimeInMin = tourEstimatedTimeInMin,
            TourType = tourTourType,
        };
        string tourToModifyJson = JsonConvert.SerializeObject(tourToModify);
        StringContent payload = new(tourToModifyJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = Client.PutAsync(endpoint, payload).Result;
        return response.IsSuccessStatusCode;
    }
    public static bool DeleteTour(Guid id)
    {
        string route = "DeleteTour?deleteID=" + id.ToString();
        Uri endpoint = new(BaseUri, route);
        HttpResponseMessage response = Client.DeleteAsync(endpoint).Result;
        return response.IsSuccessStatusCode;
    }
}