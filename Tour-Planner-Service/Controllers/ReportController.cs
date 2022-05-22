﻿namespace Tour_Planner_Service.Controllers;

[Route("Report")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly ILog log = LogManager.GetLogger(typeof(ReportController));
    private readonly IConfiguration config;
    private static readonly HttpClient client = new();
    public ReportController(IConfiguration configuration)
    {
        config = configuration;
    }
    [HttpPost("TourReport")]
    public ActionResult GetTourReport(TourDTO item)
    {
        try
        {
            if (item is null)
            {
                throw new ArgumentException("Tour is empty");
            }
            Tour newTour = new()
            {
                Id = item.Id,
                TourName = item.TourName,
                TourDescription = item.TourDescription,
                StartingPoint = item.StartingPoint,
                Destination = item.Destination,
                TransportType = item.TransportType,
                TourDistance = item.TourDistance,
                EstimatedTimeInMin = item.EstimatedTimeInMin,
                TourType = item.TourType
            };
            //StaticMap
            string staticmapParameters = $"map?key=yKBh4sWxDYGp5iebnTtjXT4YKHR3KXnE&size=300,200&start={item.StartingPoint}&end={item.Destination}&defaultMarker=none";
            Uri endpoint = new(config.GetValue<string>("Mapquest:staticmapUri") + staticmapParameters);
            byte[] image = client.GetByteArrayAsync(endpoint).Result;

            ReportGenerator.GenerateTourReport(newTour, image);
            log.Info("TourReport Generated Successfully");
            return Ok("TourReport Generated Successfully");
        }
        catch (Exception ex)
        {
            log.Fatal(ex.Message);
            return StatusCode(500);
        }
    }
    //[HttpPost("SummaryReport")]
    //public ActionResult GetSummaryReport()
    //{

    //}
}
