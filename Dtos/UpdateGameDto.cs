using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record UpdateGameDto(
    [Required][MinLength(3)][MaxLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Required][Range(1, 999.99)] decimal Price,
    DateOnly ReleaseDate
);