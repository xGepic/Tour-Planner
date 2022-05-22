namespace Tour_Planner_Service.Controllers;

[Route("Report")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly ILog log = LogManager.GetLogger(typeof(ReportController));
    private static readonly HttpClient client = new();
    private static readonly Uri staticmapUri = new("http://www.mapquestapi.com/staticmap/v5/");
    private static string staticmapParameters = string.Empty;

    public ReportController()
    {
    }
    [HttpPost("TourReport")]
    public ActionResult Post(TourDTO item)
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
            staticmapParameters = $"map?key=yKBh4sWxDYGp5iebnTtjXT4YKHR3KXnE&sizie=800,600&start={item.StartingPoint}&end={item.Destination}&defaultMarker=none";
            Uri endpoint = new(staticmapUri, staticmapParameters);
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
}
