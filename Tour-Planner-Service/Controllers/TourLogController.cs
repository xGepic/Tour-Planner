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
        [HttpGet(Name = "GetTourLogs")]
        public IEnumerable<TourLog> Get()
        {
            string SqlSDataSource = _configuration.GetConnectionString("DBConnection");
            using NpgsqlConnection myConn = new(SqlSDataSource);
            myConn.Open();
            using NpgsqlCommand myCommand = new("SELECT * FROM Tourlog", myConn);
            using NpgsqlDataReader myDataReader = myCommand.ExecuteReader();
            if (myDataReader is not null)
            {
                while (myDataReader.Read())
                {
                    yield return new TourLog(
                        myDataReader.GetDateTime(1),
                        myDataReader.GetString(2),
                        (TourDifficulty)myDataReader.GetInt32(3),
                        Convert.ToUInt32(myDataReader.GetInt32(4)),
                        (TourRating)myDataReader.GetInt32(5)
                        );
                }
                myConn.Close();
            }
            myConn.Close();
        }
    }
}
