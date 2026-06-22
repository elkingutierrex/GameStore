namespace GameStore.Api.Dtos;

public static class GameEndPoints
{
    const string EndpointName = "GetGameById";
    private static readonly List<GameDto> games = [ new GameDto(1, "Cyberpunk 2077", "Action RPG", 59.99m, new DateTime(2020, 12, 10)),
                        new GameDto(2, "The Witcher 3: Wild Hunt", "Action RPG", 39.99m, new DateTime(2015, 5, 19)),
                        new GameDto(3, "Red Dead Redemption 2", "Action Adventure", 59.99m, new DateTime(2018, 10, 26))];


    public static void MapGameEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);

            return game is not null ? Results.Ok(game) : Results.NotFound();

        }).WithName(EndpointName);

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            if (string.IsNullOrEmpty(newGame.Name))
            {
                return Results.BadRequest("Name is required.");
            }
            int newId = games.Max(game => game.Id) + 1;
            GameDto gameToAdd = new GameDto(newId, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
            games.Add(gameToAdd);
            return Results.Created($"/games/{newId}", gameToAdd);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
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

        group.MapDelete("/{id}", (int id) =>
        {
            var game = games.Find(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }

            games.Remove(game);
            return Results.NoContent();
        });
    }

}