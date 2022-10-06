using RestApiWithMongoDb.Models;
using RestApiWithMongoDb.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.Configure<DatabaseSettings>(
//     builder.Configuration.GetSection("DatabaseConnection"));

//Add database connection with singleton
builder.Services.AddSingleton<IMongoDatabase>(option=>{
    var mongoSettings= builder.Configuration.GetSection("DatabaseConnection").Get<DatabaseSettings>();
    var mongoClient = new MongoClient(mongoSettings.ConnectionString);
    return mongoClient.GetDatabase(mongoSettings.DatabaseName);
});

//List of Service added here as singleton
builder.Services.AddSingleton<BooksService>();
builder.Services.AddSingleton<PersonsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
