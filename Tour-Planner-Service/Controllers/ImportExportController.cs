namespace Tour_Planner_Service.Controllers;

[Route("ImportAndExport")]
[ApiController]
public class ImportExportController : ControllerBase
{
    private readonly ILog log = LogManager.GetLogger(typeof(ImportExportController));
    private readonly DBTour myDB;
    public ImportExportController(IConfiguration configuration)
    {
        myDB = DBTour.GetInstance(configuration);
    }
    [HttpGet("ImportTour")]
    public ActionResult<Tour> Import(string name)
    {
        try
        {
            Tour newTour = CSVGenerator.ImportTourFromCSV(name) ?? throw new ArgumentException($"Name not Found!: {name}");
            if (myDB.GetTourByID(newTour.Id) is null)
            {
                if (myDB.AddTour(newTour))
                {
                    log.Info("Tour Added Successfully: " + newTour.Id);
                    return Ok(newTour);
                }
            }
            log.Fatal("Tour already exists");
            return StatusCode(500);
        }
        catch (Exception ex)
        {
            log.Fatal(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpPost("ExportTour")]
    public ActionResult Export(TourDTO item)
    {
        try
        {
            if (item is null)
            {
                log.Fatal("Tour is empty");
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
            if (CSVGenerator.ExportTourToCSV(newTour))
            {
                log.Info("CSV Generated Successfully");
                return Ok("CSV Generated Successfully");
            }
            return StatusCode(500);
        }
        catch (Exception ex)
        {
            log.Fatal(ex.Message);
            return StatusCode(500);
        }
    }
}