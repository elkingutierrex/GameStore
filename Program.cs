using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

var app = builder.Build();
app.MapGameEndPoints();

app.Run();
