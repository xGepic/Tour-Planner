namespace Tour_Planner_Service.Controllers;

[Route("TourLog")]
[ApiController]
public class TourLogController : ControllerBase
{
    private readonly ILog log = LogManager.GetLogger(typeof(TourLogController));
    private readonly DBTourLog myDB;
    public TourLogController(IConfiguration configuration)
    {
        myDB = DBTourLog.GetInstance(configuration);
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<TourLog>> Get()
    {
        try
        {
            log.Info("Get All Tourlogs Successful!");
            return Ok(myDB.GetAllTourLogs() ?? throw new HttpRequestException());
        }
        catch (Exception ex)
        {
            log.Fatal(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpGet("GetByID")]
    public ActionResult<TourLog> Get(Guid id)
    {
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
            log.Fatal(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpPost("AddTourLog")]
    public ActionResult Post(TourLogDTO item)
    {
        try
        {
            if (!myDB.CheckRelatedTourID(item.RelatedTourID))
            {
                throw new Exception("RelatedTourID doesnt match with a Tour!");
            }
            TourLog newLog = new()
            {
                Id = Guid.NewGuid(),
                TourDateAndTime = item.TourDateAndTime,
                TourComment = item.TourComment,
                TourDifficulty = (TourDifficulty)item.TourDifficulty,
                TourTimeInMin = item.TourTimeInMin,
                TourRating = (TourRating)item.TourRating,
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
            log.Fatal(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpPut("UpdateTourLog")]
    public ActionResult Put(TourLogDTO item)
    {
        try
        {
            if (!myDB.CheckRelatedTourID(item.RelatedTourID))
            {
                throw new Exception("RelatedTourID doesnt match with a Tour!");
            }
            TourLog newLog = new()
            {
                Id = item.Id,
                TourDateAndTime = item.TourDateAndTime,
                TourComment = item.TourComment,
                TourDifficulty = (TourDifficulty)item.TourDifficulty,
                TourTimeInMin = item.TourDifficulty,
                TourRating = (TourRating)item.TourRating,
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
            log.Fatal(ex.Message);
            return StatusCode(500);
        }
    }
    [HttpDelete("DeleteTourLog")]
    public ActionResult DeleteTourLog(Guid deleteID)
    {
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
            log.Fatal(ex.Message);
            return StatusCode(500);
        }
    }
}
