var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Logger
builder.Logging.AddLog4Net("log4net.config");

//Build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Configure File Path for File Uploads
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/Uploads"
});

//Set Database Startup Methods 
DB_Startup startUpDB = new(builder.Configuration, true);
startUpDB.CreateDatabase();
DB_Startup defaultDB = new(builder.Configuration);
defaultDB.CreateTables();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();