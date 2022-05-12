namespace TourPlannerTests;

internal class TourLogControllerTests
{
    private static readonly Dictionary<string, string> configForController = new() { { "ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=5432;Database=tourplanner;User Id=postgres;Password=asdf;" }, };
    private static readonly IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(configForController).Build();
    //UnitOfWork_StateUnderTest_ExpectedBehavior
    [Test]
    public void GetAll_WithNoExistingTour_ShouldReturnNotFound()
    {
        //Arrange
        var controller = new TourLogController(configuration);
        Guid id = Guid.NewGuid();

        //Act
        var actionResult = controller.Get(id);
        var result = actionResult.Result as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
    [Test]
    public void AddTourLog_WithExistingTour_ShouldReturnNotFound()
    {
        //Arrange
        var controller = new TourLogController(configuration);
        TourLogDTO testTourLog = new()
        {
            Id = Guid.NewGuid(),
            TourDateAndTime = DateTime.Now,
            TourComment = null,
            TourDifficulty = 2,
            TourTimeInMin = 600,
            TourRating = 3,
            RelatedTourID = Guid.NewGuid()
        };

        //Act
        var actionResult = controller.Post(testTourLog);
        var result = actionResult as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
}