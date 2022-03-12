namespace Tour_Planner_Service.Controllers;

[Route("Tour")]
[ApiController]
public class TourController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILog log = LogManager.GetLogger(typeof(TourController));
    public TourController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<Tour>> Get()
    {
        DBConnection myDB = new(_configuration);
        try
        {
            return Ok(myDB.GetAllTours() ?? throw new HttpRequestException());
        }
        catch
        {
            return StatusCode(500);
        }
    }
    [HttpGet("GetByID")]
    public ActionResult<Tour> Get(Guid id)
    {
        DBConnection myDB = new(_configuration);
        try
        {
            if (myDB.GetTourByID(id) is null)
            {
                return NotFound();
            }
            return Ok(myDB.GetTourByID(id));
        }
        catch
        {
            return StatusCode(500);
        }
    }
    [HttpPost("AddTour")]
    public ActionResult Post(TourDTO item)
    {
        DBConnection myDB = new(_configuration);
        try
        {
            Tour newTour = new()
            {
                Id = Guid.NewGuid(),
                TourName = item.Name,
                TourDescription = item.Description,
                StartingPoint = item.StartingPoint,
                Destination = item.Destination,
                TransportType = item.TransportType,
                TourDistance = item.TourDistance,
                EstimatedTimeInMin = item.EstimatedTimeInMin,
                TourType = item.TourType
            };
            if (myDB.AddTour(newTour))
            {
                return new JsonResult("Added Successfully!");
            }
            throw new HttpRequestException();
        }
        catch
        {
            return StatusCode(500);
        }
    }
    [HttpPut("UpdateTour")]
    public ActionResult Put(TourDTO item)
    {
        DBConnection myDB = new(_configuration);
        try
        {
            Tour newTour = new()
            {
                Id = item.ID,
                TourName = item.Name,
                TourDescription = item.Description,
                StartingPoint = item.StartingPoint,
                Destination = item.Destination,
                TransportType = (TransportType)item.TransportType,
                TourDistance = item.TourDistance,
                EstimatedTimeInMin = item.EstimatedTimeInMin,
                TourType = (Tourtype)item.TourType
            };
            Tour? existingItem = myDB.GetTourByID(newTour.Id);
            if (existingItem is null)
            {
                return NotFound();
            }
            if (myDB.UpdateTour(newTour))
            {
                return new JsonResult("Updated Successfully!");
            }
            throw new HttpRequestException();
        }
        catch
        {
            return StatusCode(500);
        }
    }
    [HttpDelete("DeleteTour")]
    public ActionResult DeleteTour(Guid deleteID)
    {
        DBConnection myDB = new(_configuration);
        try
        {
            Tour? existingItem = myDB.GetTourByID(deleteID);
            if (existingItem is null)
            {
                return NotFound();
            }
            if (myDB.DeleteTour(deleteID))
            {
                return new JsonResult("Deleted Successfully!");
            }
            return new JsonResult("Delete Failed!");
        }
        catch
        {
            return StatusCode(500);
        }
    }
}
