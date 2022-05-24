namespace Tour_Planner_UI.Repositories;
internal class ReportRepository
{
    private static readonly HttpClient Client = new();
    private static readonly Uri BaseUri = new("https://localhost:7122/Report/");
    public static bool TourReport(Tour tour)
    {
        Uri endpoint = new(BaseUri, "TourReport");
        string tourJson = JsonConvert.SerializeObject(tour);
        StringContent payload = new(tourJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = Client.PostAsync(endpoint, payload).Result;
        return response.IsSuccessStatusCode;
    }
    public static bool SummaryReport()
    {
        Uri endpoint = new(BaseUri, "SummaryReport");
        HttpResponseMessage response = Client.PostAsync(endpoint, null).Result;
        return response.IsSuccessStatusCode;
    }
}
