namespace Tour_Planner_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourLogController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TourLogController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet(Name = "GetAllTourLogs")]
        public IEnumerable<TourLog> Get()
        {
            DBConnection myDB = new(_configuration);
            return myDB.GetAllTourLogs();
        }
    }
}
