namespace Tour_Planner_DB;

public partial class DBConnection
{
    private readonly NpgsqlConnection connection = new();
    private readonly IConfiguration _configuration;
    public DBConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        string SqlSDataSource = _configuration.GetConnectionString("DBConnection");
        connection = new(SqlSDataSource);
    }
    private void Open()
    {
        connection.Open();
    }
    private void Close()
    {
        connection.Close();
    }

}