namespace Tour_Planner_Service.Controllers;

[Route("TourLog")]
[ApiController]
public class TourLogController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILog log = LogManager.GetLogger(typeof(TourController));
    public TourLogController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<TourLog>> Get()
    {
        DBTourLog myDB = new(_configuration);
        try
        {
            log.Info("Get All Tourlogs Successful!");
            return Ok(myDB.GetAllTourLogs() ?? throw new HttpRequestException());
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpGet("GetByID")]
    public ActionResult<TourLog> Get(Guid id)
    {
        DBTourLog myDB = new(_configuration);
        try
        {
            if (myDB.GetTourLogByID(id) is null)
            {
                log.Error("TourLog Not Found: " + id);
                return NotFound();
            }
            log.Info("Get TourLog By ID Successful: " + id);
            return Ok(myDB.GetTourLogByID(id));
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpPost("AddTourLog")]
    public ActionResult Post(TourLogDTO item)
    {
        DBTourLog myDB = new(_configuration);
        try
        {
            if (!myDB.CheckRelatedTourID(item.RelatedTourID))
            {
                throw new Exception("RelatedTourID doesnt match with a Tour!");
            }
            TourLog newLog = new()
            {
                Id = Guid.NewGuid(),
                TourDateAndTime = item.DateAndTime,
                TourComment = item.Comment,
                TourDifficulty = (TourDifficulty)item.Difficulty,
                TourTimeInMin = item.TimeInMin,
                TourRating = (TourRating)item.Rating,
                RelatedTourID = item.RelatedTourID
            };
            if (myDB.AddTourLog(newLog))
            {
                log.Info("TourLog Added Successfully: " + item.Id);
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
    [HttpPut("UpdateTourLog")]
    public ActionResult Put(TourLogDTO item)
    {
        DBTourLog myDB = new(_configuration);
        try
        {
            if (!myDB.CheckRelatedTourID(item.RelatedTourID))
            {
                throw new Exception("RelatedTourID doesnt match with a Tour!");
            }
            TourLog newLog = new()
            {
                Id = item.Id,
                TourDateAndTime = item.DateAndTime,
                TourComment = item.Comment,
                TourDifficulty = (TourDifficulty)item.Difficulty,
                TourTimeInMin = item.TimeInMin,
                TourRating = (TourRating)item.Rating,
                RelatedTourID = item.RelatedTourID
            };
            TourLog? existingItem = myDB.GetTourLogByID(newLog.Id);
            if (existingItem is null)
            {
                log.Error("TourLog Not Found: " + item.Id);
                return NotFound();
            }
            if (myDB.UpdateTourLog(newLog))
            {
                log.Info("TourLog Updated Successfully: " + item.Id);
                return new JsonResult("Updated Successfully!");
            }
            return new JsonResult("Update Failed!");
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpDelete("DeleteTourLog")]
    public ActionResult DeleteTourLog(Guid deleteID)
    {
        DBTourLog myDB = new(_configuration);
        try
        {
            TourLog? existingItem = myDB.GetTourLogByID(deleteID);
            if (existingItem is null)
            {
                log.Error("TourLog Not Found: " + deleteID);
                return NotFound();
            }
            if (myDB.DeleteTourLog(deleteID))
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
}
