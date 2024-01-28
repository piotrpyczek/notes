using Notes.API.Infrastructure;
using Notes.API.Infrastructure.AppConext;
using Notes.API.Infrastructure.Database;
using Notes.API.Infrastructure.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddBearerAuthorization();
});

builder.Services.AddApplicationConfiguration(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAppContext();

app.MapControllers();

app.MigrateDatabase();

app.Run();


public partial class Program { }