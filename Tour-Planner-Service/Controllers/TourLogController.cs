namespace Tour_Planner_Service.Controllers
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
        public ActionResult Get()
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
        public ActionResult Get(Guid id)
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
    }
}
