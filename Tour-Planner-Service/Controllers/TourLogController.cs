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
        public IEnumerable<TourLog> Get()
        {
            DBConnection myDB = new(_configuration);
            return myDB.GetAllTourLogs();
        }
        [HttpGet("GetByID")]
        public ActionResult Get(Guid id)
        {
            DBConnection myDB = new(_configuration);
            try
            {
                if(myDB.GetTourLogByID(id) is null)
                {
                    throw new HttpRequestException();
                }
                return Ok(myDB.GetTourLogByID(id));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
