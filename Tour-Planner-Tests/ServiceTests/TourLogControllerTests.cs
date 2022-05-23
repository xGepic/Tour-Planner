namespace TourPlannerTests;

internal class TourLogControllerTests
{
    private static readonly Dictionary<string, string> configForController = new() { { "ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=5432;Database=tourplanner;User Id=postgres;Password=asdf;" }, };
    private static readonly IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(configForController).Build();
    private static readonly TourLogDTO testTourLog = new()
    {
        Id = Guid.NewGuid(),
        TourDateAndTime = DateTime.Now,
        TourComment = null,
        TourDifficulty = TourDifficulty.medium,
        TourTimeInMin = 600,
        TourRating = TourRating.neutral,
        RelatedTourID = Guid.NewGuid()
    };
    //UnitOfWork_StateUnderTest_ExpectedBehavior
    [Test]
    public void GetAll_WithNoExistingTour_ReturnsNotFound()
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
    public void AddTourLog_WithoutExistingTour_ReturnsNotFound()
    {
        //Arrange
        var controller = new TourLogController(configuration);

        //Act
        var actionResult = controller.Post(testTourLog);
        var result = actionResult as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
    [Test]
    public void UpdateTourLog_WithNoExistingTourLog_ReturnsInternalServerError()
    {
        //Arrange
        var controller = new TourLogController(configuration);

        //Act
        var actionResult = controller.Put(testTourLog);
        var result = actionResult as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
    [Test]
    public void UpdateTourLog_WithIncompleteTour_ReturnsInternalServerError()
    {
        //Arrange
        var controller = new TourLogController(configuration);
        TourLogDTO incompleteTourlog = new()
        {
            Id = Guid.NewGuid()
        };

        //Act
        var actionResult = controller.Put(incompleteTourlog);
        var result = actionResult as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
    [Test]
    public void DeleteTourLog_WithNoExistingTourLog_ReturnsNotFound()
    {
        //Arrange
        var controller = new TourLogController(configuration);

        //Act
        var actionResult = controller.DeleteTourLog(testTourLog.Id);
        var result = actionResult as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
}