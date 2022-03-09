namespace Tour_Planner_Service.Controllers;

[Route("TourLog")]
[ApiController]
public class TourLogController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public TourLogController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<TourLog>> Get()
    {
        DBConnection myDB = new(_configuration);
        try
        {
            return Ok(myDB.GetAllTourLogs() ?? throw new HttpRequestException());
        }
        catch
        {
            return StatusCode(500);
        }
    }
    [HttpGet("GetByID")]
    public ActionResult<TourLog> Get(Guid id)
    {
        DBConnection myDB = new(_configuration);
        try
        {
            if (myDB.GetTourLogByID(id) is null)
            {
                return NotFound();
            }
            return Ok(myDB.GetTourLogByID(id));
        }
        catch
        {
            return StatusCode(500);
        }
    }
    [HttpPost("AddTourLog")]
    public ActionResult Post(TourLogDTO item)
    {
        DBConnection myDB = new(_configuration);
        try
        {
            if (item.Comment is null)
            {
                item.Comment = "";
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
                return new JsonResult("Added Successfully!");
            }
            throw new HttpRequestException();
        }
        catch
        {
            return StatusCode(500);
        }
    }
    [HttpPut("UpdateTourLog")]
    public ActionResult Put(TourLogDTO item)
    {
        DBConnection myDB = new(_configuration);
        try
        {
            if (item.Comment is null)
            {
                item.Comment = "";
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
                return NotFound();
            }
            if (myDB.UpdateTourLog(newLog))
            {
                return new JsonResult("Updated Successfully!");
            }
            return new JsonResult("Update Failed!");
        }
        catch
        {
            return StatusCode(500);
        }
    }
    [HttpDelete]
    public ActionResult DeleteTourLog(Guid deleteID)
    {
        DBConnection myDB = new(_configuration);
        try
        {
            TourLog? existingItem = myDB.GetTourLogByID(deleteID);
            if (existingItem is null)
            {
                return NotFound();
            }
            if (myDB.DeleteTourLog(deleteID))
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
