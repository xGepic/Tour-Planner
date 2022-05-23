namespace Tour_Planner_UI.Repositories;
internal class ImportExportRepository
{
    private static readonly HttpClient Client = new();
    private static readonly Uri BaseUri = new("https://localhost:7122/ImportAndExport/");
    public static Tour ImportTour(string name)
    {
        Uri endpoint = new(BaseUri, "ImportTour");
        StringContent payload = new(name, Encoding.UTF8, "application/json");
        HttpResponseMessage response = Client.PostAsync(endpoint, payload).Result;
        string resultAsString = response.Content.ReadAsStringAsync().Result;
        Tour? tour = JsonConvert.DeserializeObject<Tour>(resultAsString);
        return tour;
    }
    public static bool ExportTour(Tour tourToExport)
    {
        Uri endpoint = new(BaseUri, "ExportTour");
        string tourJson = JsonConvert.SerializeObject(tourToExport);
        StringContent payload = new(tourJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = Client.PostAsync(endpoint, payload).Result;
        return response.IsSuccessStatusCode;
    }
}
