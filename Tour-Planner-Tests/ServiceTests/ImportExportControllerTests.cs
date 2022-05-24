namespace Tour_Planner_Tests;

internal class ImportExportControllerTests
{
    private static readonly Dictionary<string, string> configForController = new() { { "ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=5432;Database=tourplanner;User Id=postgres;Password=asdf;" }, };
    private static readonly IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(configForController).Build();
    [Test]
    public void Import_WithNonexistingTour_ReturnsInternalServerError()
    {
        //Arrange
        var controller = new ImportExportController(configuration);
        string nonexistentTour = "nonexistentTour";

        //Act
        var actionResult = controller.Import(nonexistentTour);
        var result = actionResult.Result as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
    [Test]
    public void Export_WhenTourIsNull_ReturnsInternalServerError()
    {
        //Arrange
        var controller = new ImportExportController(configuration);
        TourDTO testTour = null;

        //Act
        var actionResult = controller.Export(testTour);
        var result = actionResult as StatusCodeResult;

        //Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}
