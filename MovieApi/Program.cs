using MovieApi;
using MovieApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add FileUtils as a singleton service
// In the future it will be easy to replace FileUtils
// with other class that operate on the specified database
// (no need to change in others units)
builder.Services.AddSingleton<IUtils, FileUtils>();

// Add movies list as a singleton service
builder.Services.AddSingleton(provider =>
{
    var fileUtils = provider.GetRequiredService<IUtils>();
    return fileUtils.LoadMovies();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();