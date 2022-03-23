namespace Tour_Planner_Service.Controllers;

[Route("Tour")]
[ApiController]
public class TourController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly ILog log = LogManager.GetLogger(typeof(TourController));
    public TourController(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<Tour>> Get()
    {
        DBTour myDB = DBTour.GetInstance(_configuration);
        try
        {
            log.Info("Get All Tours Successful!");
            return Ok(DBTour.GetAllTours() ?? throw new HttpRequestException());
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpGet("GetByID")]
    public ActionResult<Tour> Get(Guid id)
    {
        DBTour myDB = DBTour.GetInstance(_configuration);
        try
        {
            if (DBTour.GetTourByID(id) is null)
            {
                log.Error("Tour Not Found: " + id);
                return NotFound();
            }
            log.Info("Get Tour By ID Successful: " + id);
            return Ok(DBTour.GetTourByID(id));
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpPost("AddTour")]
    public ActionResult Post(TourDTO item)
    {
        DBTour myDB = DBTour.GetInstance(_configuration);
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
            if (DBTour.AddTour(newTour))
            {
                log.Info("Tour Added Successfully: " + newTour.Id);
                return new JsonResult("Added Successfully!");
            }
            throw new HttpRequestException();
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpPut("UpdateTour")]
    public ActionResult Put(TourDTO item)
    {
        DBTour myDB = DBTour.GetInstance(_configuration);
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
            Tour? existingItem = DBTour.GetTourByID(newTour.Id);
            if (existingItem is null)
            {
                log.Error("Tour Not Found: " + item.ID);
                return NotFound();
            }
            if (DBTour.UpdateTour(newTour))
            {
                log.Info("Tour Updated Successfully: " + item.ID);
                return new JsonResult("Updated Successfully!");
            }
            throw new HttpRequestException();
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpDelete("DeleteTour")]
    public ActionResult DeleteTour(Guid deleteID)
    {
        DBTour myDB = DBTour.GetInstance(_configuration);
        try
        {
            Tour? existingItem = DBTour.GetTourByID(deleteID);
            if (existingItem is null)
            {
                log.Error("Tour Not Found: " + deleteID);
                return NotFound();
            }
            if (DBTour.DeleteTour(deleteID))
            {
                log.Info("Tour Deleted Successfully: " + deleteID);
                return new JsonResult("Deleted Successfully!");
            }
            return new JsonResult("Delete Failed!");
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpPost("SaveFile")]
    public JsonResult SaveFile()
    {
        try
        {
            var httpRequest = Request.Form;
            var postedFile = httpRequest.Files[0];
            if (postedFile.ContentType.Contains("image"))
            {
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Uploads/" + filename;
                using var stream = new FileStream(physicalPath, FileMode.Create);
                postedFile.CopyTo(stream);
                log.Info("New File Uploaded: " + filename);
                return new JsonResult("Uploaded Successfully: " + filename);
            }
            throw new FileLoadException();
        }
        catch (Exception ex)
        {
            log.Error(ex);
            return new JsonResult("Upload Failed");
        }
    }
}
