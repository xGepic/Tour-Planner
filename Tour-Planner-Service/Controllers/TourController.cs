namespace Tour_Planner_Service.Controllers;

[Route("Tour")]
[ApiController]
public class TourController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    public TourController(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }
}
