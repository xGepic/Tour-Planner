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

//Set Database Startup Methods 
DB_Startup myDB = new(builder.Configuration);
myDB.CreateDatabase();
myDB.CreateTables();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();