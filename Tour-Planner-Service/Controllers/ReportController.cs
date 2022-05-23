namespace Tour_Planner_Service.Controllers;

[Route("Report")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly ILog log = LogManager.GetLogger(typeof(ReportController));
    private readonly IConfiguration config;
    private readonly DBTour myDB;
    private static readonly HttpClient client = new();
    public ReportController(IConfiguration configuration)
    {
        config = configuration;
        myDB = DBTour.GetInstance(configuration);
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
                TourType = item.TourType,
                TourLogs = item.TourLogs
            };
            //StaticMap
            string staticmapParameters = $"&start={item.StartingPoint}&end={item.Destination}&defaultMarker=none";
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
    [HttpPost("SummaryReport")]
    public ActionResult GetSummaryReport()
    {
        try
        {
            IEnumerable<Tour> allTours = myDB.GetAllTours() ?? throw new HttpRequestException();
            ReportGenerator.GenerateSummaryReport(allTours);
            log.Info("SummaryReport Generated Successfully");
            return Ok("SummaryReport Generated Successfully");
        }
        catch (Exception ex)
        {
            log.Fatal(ex.Message);
            return StatusCode(500);
        }
    }
}
