namespace Tour_Planner_Service.Controllers;

[Route("Tour")]
[ApiController]
public class TourController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public TourController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
