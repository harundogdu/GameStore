using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);