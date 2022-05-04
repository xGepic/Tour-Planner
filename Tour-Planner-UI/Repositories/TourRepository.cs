﻿namespace Tour_Planner_UI;

internal static class TourRepository
{
    private static readonly HttpClient _Client = new();
    private static readonly Uri _BaseUri = new("https://localhost:7122/Tour/");
    public static Tour[]? GetAllTours()
    {
        Uri endpoint = new(_BaseUri, "GetAll");
        HttpResponseMessage Response = _Client.GetAsync(endpoint).Result;
        string ResultAsString = Response.Content.ReadAsStringAsync().Result;
        Tour[]? AllTours = JsonConvert.DeserializeObject<Tour[]>(ResultAsString);
        return AllTours;
    }
    //public static Tour? GetTourById()
    //{
    //    Uri endpoint = new(_BaseUri, "GetByID");
    //    HttpResponseMessage Response = _Client.GetAsync(endpoint).Result;
    //    string ResultAsString = Response.Content.ReadAsStringAsync().Result;
    //    Tour? Tour = JsonConvert.DeserializeObject<Tour>(ResultAsString);
    //    return Tour;
    //}
    public static bool AddTour(string TourName, string TourDescription, string TourStartingPoint, string TourDestination, TransportType TourTransportType, TourType TourTourType, double TourTourDistance, uint TourEstimatedTimeInMin)
    {
        Uri endpoint = new(_BaseUri, "AddTour");
        TourDTO TourToAdd = new()
        {
            TourName = TourName,
            TourDescription = TourDescription,
            StartingPoint = TourStartingPoint,
            Destination = TourDestination,
            TransportType = TourTransportType,
            TourDistance = TourTourDistance,
            EstimatedTimeInMin = TourEstimatedTimeInMin,
            TourType = TourTourType,
        };
        string TourToAddJson = JsonConvert.SerializeObject(TourToAdd);
        StringContent payload = new(TourToAddJson, Encoding.UTF8, "application/json");
        HttpResponseMessage Response = _Client.PostAsync(endpoint, payload).Result;
        return Response.IsSuccessStatusCode;
    }
    public static bool UpdateTour()
    {
        Uri endpoint = new(_BaseUri, "UpdateTour");
        HttpResponseMessage Response = _Client.GetAsync(endpoint).Result;
        return Response.IsSuccessStatusCode;
    }
    public static bool DeleteTour(Guid Id)
    {
        string route = "DeleteTour?deleteID=" + Id.ToString();
        Uri endpoint = new(_BaseUri, route);
        HttpResponseMessage Response = _Client.DeleteAsync(endpoint).Result;
        return Response.IsSuccessStatusCode;
    }
    public static bool SaveFile()
    {
        Uri endpoint = new(_BaseUri, "SaveFile");
        HttpResponseMessage Response = _Client.GetAsync(endpoint).Result;
        return Response.IsSuccessStatusCode;
    }
}