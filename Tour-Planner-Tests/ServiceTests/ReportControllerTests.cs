namespace Tour_Planner_Tests;

internal class ReportControllerTests
{
    private static readonly Dictionary<string, string> configForController = new() { { "ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=5432;Database=tourplanner;User Id=postgres;Password=asdf;" }, };
    private static readonly IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(configForController).Build();
    [Test]
    public void GetTourReport_WhenTourIsNull_ReturnsInternalServerError()
    {
        //Arrange
        var controller = new ReportController(configuration);
        TourDTO testTour = null;

        //Act
        var actionResult = controller.GetTourReport(testTour);
        var result = actionResult as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}