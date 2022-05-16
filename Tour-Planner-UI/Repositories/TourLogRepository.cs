namespace Tour_Planner_UI.Repositories;
internal class TourLogRepository
{
    private static readonly HttpClient Client = new();
    private static readonly Uri BaseUri = new("https://localhost:7122/TourLog/");
    public static TourLog[]? GetAllTourLogs(Guid relatedId)
    {
        string route = "GetAll?id=" + relatedId.ToString();
        Uri endpoint = new(BaseUri, route);
        HttpResponseMessage response = Client.GetAsync(endpoint).Result;
        string resultAsString = response.Content.ReadAsStringAsync().Result;
        TourLog[]? allTourLogs = JsonConvert.DeserializeObject<TourLog[]>(resultAsString);
        return allTourLogs;
    }
    public static TourLog? GetTourLogById(Guid id)
    {
        string route = "GetByID?id=" + id.ToString();
        Uri endpoint = new(BaseUri, route);
        HttpResponseMessage response = Client.GetAsync(endpoint).Result;
        string resultAsString = response.Content.ReadAsStringAsync().Result;
        TourLog? tourLog = JsonConvert.DeserializeObject<TourLog>(resultAsString);
        return tourLog;
    }
    public static bool AddTourLog(DateTime tourDateAndTime, string? tourComment, TourDifficulty tourDifficulty, uint tourTimeInMin, TourRating tourRating, Guid relatedTourID)
    {
        Uri endpoint = new(BaseUri, "AddTourLog");
        TourLogDTO tourLogToAdd = new()
        {
            TourDateAndTime = tourDateAndTime,
            TourComment = tourComment,
            TourDifficulty = tourDifficulty,
            TourTimeInMin = tourTimeInMin,
            TourRating = tourRating,
            RelatedTourID = relatedTourID
        };
        string tourLogToAddJson = JsonConvert.SerializeObject(tourLogToAdd);
        StringContent payload = new(tourLogToAddJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = Client.PostAsync(endpoint, payload).Result;
        return response.IsSuccessStatusCode;
    }
    public static bool UpdateTourLog(Guid id, DateTime tourDateAndTime, string? tourComment, TourDifficulty tourDifficulty, uint tourTimeInMin, TourRating tourRating, Guid relatedTourID)
    {
        Uri endpoint = new(BaseUri, "UpdateTourLog");
        TourLogDTO tourLogToModify = new()
        {
            Id = id,
            TourDateAndTime = tourDateAndTime,
            TourComment = tourComment,
            TourDifficulty = tourDifficulty,
            TourTimeInMin = tourTimeInMin,
            TourRating = tourRating,
            RelatedTourID = relatedTourID
        };
        string tourLogToModifyJson = JsonConvert.SerializeObject(tourLogToModify);
        StringContent payload = new(tourLogToModifyJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = Client.PutAsync(endpoint, payload).Result;
        return response.IsSuccessStatusCode;
    }
    public static bool DeleteTourLog(Guid id)
    {
        string route = "DeleteTourLog?deleteID=" + id.ToString();
        Uri endpoint = new(BaseUri, route);
        HttpResponseMessage response = Client.DeleteAsync(endpoint).Result;
        return response.IsSuccessStatusCode;
    }
}
