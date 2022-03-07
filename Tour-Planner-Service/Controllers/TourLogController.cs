﻿namespace Tour_Planner_Service.Controllers
{
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
                return Ok(myDB.GetTourLogByID(id) ?? throw new HttpRequestException());
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
                TourLog newLog = new(Guid.NewGuid(), item.DateAndTime, item.Comment, (TourDifficulty)item.Difficulty, item.TimeInMin, (TourRating)item.Rating);
                if (myDB.AddTourLog(newLog))
                {
                    return new JsonResult("Added Successfully");
                }
                throw new HttpRequestException();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
