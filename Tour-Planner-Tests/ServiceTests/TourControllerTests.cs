namespace TourPlannerTests;

internal class TourControllerTests
{
    private static readonly Dictionary<string, string> configForController = new() { { "ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=5432;Database=tourplanner;User Id=postgres;Password=asdf;" }, };
    private static readonly IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(configForController).Build();
    private static readonly TourDTO testTour = new()
    {
        Id = Guid.NewGuid(),
        TourName = "Test Tour",
        TourDescription = "Test",
        StartingPoint = "Berlin",
        Destination = "Wien",
        TransportType = Tour_Planner_Model.TransportType.byCar,
        TourDistance = 600,
        EstimatedTimeInMin = 300,
        TourType = TourType.Vacation
    };
    //UnitOfWork_StateUnderTest_ExpectedBehavior
    [Test]
    public void GetAll_WhenDBisNotEmpty_ReturnsNotNull()
    {
        //Arrange
        var controller = new TourController(configuration);

        //Act
        var actionResult = controller.Get();
        var result = actionResult.Result as OkObjectResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }
    [Test]
    public void GetByID_WhenIDIsNotThere_ReturnsNotFound()
    {
        //Arrange
        var controller = new TourController(configuration);
        Guid id = Guid.NewGuid();

        //Act
        var actionResult = controller.Get(id);
        var result = actionResult.Result as NotFoundResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
    [OneTimeTearDown]
    public void TearDown()
    {
        DBTour myDB = DBTour.GetInstance(configuration);
        myDB.DeleteTourByName("Test Tour");
    }
    [Test]
    public void AddTour_WithNewTour_ReturnsSuccess()
    {
        //Arrange
        var controller = new TourController(configuration);

        //Act
        var actionResult = controller.Post(testTour);
        var result = actionResult as OkObjectResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }
    [Test]
    public void UpdateTour_WithNoExistingTour_ReturnsNotFound()
    {
        //Arrange
        var controller = new TourController(configuration);

        //Act
        var actionResult = controller.Put(testTour);
        var result = actionResult as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
    [Test]
    public void DeleteTour_WithNoExistingTour_ReturnsNotFound()
    {
        //Arrange
        var controller = new TourController(configuration);

        //Act
        var actionResult = controller.DeleteTour(testTour.Id);
        var result = actionResult as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
}