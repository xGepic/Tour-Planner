namespace TourPlannerTests;

internal class TourControllerTests
{
    private static readonly Dictionary<string, string> configForController = new() { { "ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=5432;Database=tourplanner;User Id=postgres;Password=asdf;" }, };
    private static readonly IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(configForController).Build();
    private static readonly Mock<IWebHostEnvironment> mockEnvironment = new();
    //UnitOfWork_StateUnderTest_ExpectedBehavior
    [Test]
    public void GetAll_WhenDBisNotEmpty_ReturnsNotNull()
    {
        //Assert
        mockEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
        var controller = new TourController(configuration, mockEnvironment.Object);

        //Act
        var actionResult = controller.Get();

        //Assert
        actionResult.Should().NotBeNull();
    }
    [Test]
    public void GetByID_WhenIDIsNotThere_ShouldReturn404()
    {
        //Assert
        mockEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
        var controller = new TourController(configuration, mockEnvironment.Object);
        Guid id = Guid.NewGuid();

        //Act
        var actionResult = controller.Get(id);
        var result = actionResult.Result as NotFoundResult;
        int resultCode = result.StatusCode;

        //Assert
        resultCode.Should().Be(404);
    }
    [OneTimeTearDown]
    public void TearDown()
    {
        DBTour myDB = DBTour.GetInstance(configuration);
        myDB.DeleteTourByName("Test Tour");
    }
    [Test]
    public void AddTour_WithNewTour_ShouldReturnSuccess()
    {
        //Assert
        mockEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
        var controller = new TourController(configuration, mockEnvironment.Object);

        //Act
        TourDTO testTour = new()
        {
            Id = Guid.NewGuid(),
            TourName = "Test Tour",
            TourDescription = "Test",
            StartingPoint = "Berlin",
            Destination = "Wien",
            TransportType = TransportType.byCar,
            TourDistance = 600,
            EstimatedTimeInMin = 300,
            TourType = TourType.Vacation
        };
        var actionResult = controller.Post(testTour);
        var result = actionResult as JsonResult;
        var value = result.Value;

        //Assert
        value.Should().Be("Added Successfully!");
    }
}