namespace GameStore.Api.Dtos;

public record CreateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate
);