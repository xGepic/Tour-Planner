namespace TourPlannerTests;

internal class TourLogControllerTests
{
    private static readonly Dictionary<string, string> configForController = new() { { "ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=5432;Database=tourplanner;User Id=postgres;Password=asdf;" }, };
    private static readonly IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(configForController).Build();    
    //UnitOfWork_StateUnderTest_ExpectedBehavior
    [Test]
    public void GetAll_WhenDBisNotEmpty_ReturnsNotNull()
    {
        //Assert
        var controller = new TourLogController(configuration);

        //Act
        var actionResult = controller.Get();

        //Assert
        actionResult.Should().NotBeNull();
    }
    [Test]
    public void GetByID_WhenIDIsNotThere_ShouldReturn404()
    {
        //Assert
        var controller = new TourLogController(configuration);
        Guid id = Guid.NewGuid();

        //Act
        var actionResult = controller.Get(id);
        var result = actionResult.Result as NotFoundResult;
        int resultCode = result.StatusCode;

        //Assert
        resultCode.Should().Be(404);
    }
}