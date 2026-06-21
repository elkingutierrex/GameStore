using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [ new GameDto(1, "Cyberpunk 2077", "Action RPG", 59.99m, new DateTime(2020, 12, 10)),
                        new GameDto(2, "The Witcher 3: Wild Hunt", "Action RPG", 39.99m, new DateTime(2015, 5, 19)),
                        new GameDto(3, "Red Dead Redemption 2", "Action Adventure", 59.99m, new DateTime(2018, 10, 26))];

app.MapGet("/games", () => games);

const string EndpointName = "GetGameById";

app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(EndpointName);

app.MapPost("/games", (CreateGameDto newGame) =>
{
    int newId = games.Max(game => game.Id) + 1;
    GameDto gameToAdd = new GameDto(newId, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
    games.Add(gameToAdd);
    return Results.Created($"/games/{newId}", gameToAdd);
});

app.MapPut("/games/{id}", (int id, CreateGameDto updatedGame) =>
{
    var game = games.Find(g => g.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }

    GameDto updatedGameDto = new GameDto(id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate);
    int index = games.IndexOf(game);
    games[index] = updatedGameDto;

    return Results.NoContent();
});

app.MapDelete("/games/{id}", (int id) =>
{
    var game = games.Find(g => g.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }

    games.Remove(game);
    return Results.NoContent();
});

app.Run();
