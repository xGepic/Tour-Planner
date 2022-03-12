namespace Tour_Planner_UI;

internal class RequestHandler
{
    [return: MaybeNull]
    public Tour_Planner_Model.TourLog[] GetAllTourLogs()
    {
        using (HttpClient client = new HttpClient())
        {
            Uri endpoint = new Uri("https://localhost:7122/TourLog/GetAll");
            HttpResponseMessage Response = client.GetAsync(endpoint).Result;
            string ResultAsString = Response.Content.ReadAsStringAsync().Result;
            Tour_Planner_Model.TourLog[]? AllTourLogs = JsonConvert.DeserializeObject<Tour_Planner_Model.TourLog[]>(ResultAsString);
            return AllTourLogs;
        }
    }
}
