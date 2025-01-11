using System.Data.Common;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetMainEndpointName = "GetMain";
    const string GetGameEndpointName = "GetGame";
    const string GetGamesEndpointName = "GetGames";
    const string PostGameEndpointName = "PostName";
    const string PutGameEndpointName = "PutGame";
    const string DeleteGameEndpointName = "DeleteGame";
    private static readonly List<GameSummaryDto> games = [
    new(
        1,
        "The Legend of Zelda: Breath of the Wild",
        "Action-adventure",
        59.99m,
        new DateOnly(2017, 3, 3)
    ),
    new(
        2,
        "Super Mario Odyssey",
        "Platformer",
        59.99m,
        new DateOnly(2017, 10, 27)
    ),
    new(
        3,
        "Mario Kart 8 Deluxe",
        "Racing",
        59.99m,
        new DateOnly(2017, 4, 28)
    ),
    new(
        4,
        "Splatoon 2",
        "Shooter",
        59.99m,
        new DateOnly(2017, 7, 21)
    ),
    new(
        5,
        "Animal Crossing: New Horizons",
        "Life simulation",
        59.99m,
        new DateOnly(2020, 3, 20)
    ),
    new(
        6,
        "Pokémon Sword and Shield",
        "Role-playing",
        59.99m,
        new DateOnly(2019, 11, 15)
    ),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();
        // GET: Get all games
        group.MapGet("/", (GameStoreContext dbContext) => dbContext.Games
            .Include(game => game.Genre)
            .Select(game => game.ToGameSummaryDto())
            .AsNoTracking()
            .ToListAsync())
            .WithName(GetGamesEndpointName);

        // GET: Get a game by id
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            var game = dbContext.Games.Find(id);
            if (game is not null)
            {
                return Results.Ok(game.ToGameDetailsDto());
            }

            return Results.NotFound();
        }).WithName(GetGameEndpointName);

        // POST: Create a new game
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToGameDetailsDto()
                );

        }).WithName(PostGameEndpointName);

        // PUT: Update game by id
        group.MapPut("/{id}", (int Id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
        {
            var game = dbContext.Games.Find(Id);
            if (game is not null)
            {
                dbContext.Entry(game).CurrentValues.SetValues(updateGame.ToEntity(Id));
                dbContext.SaveChanges();
                return Results.NoContent();
            }

            return Results.NotFound();

        }).WithName(PutGameEndpointName);

        // DELETE: Delete a game by id
        group.MapDelete("/{id}", (int Id, GameStoreContext dbContext) =>
        {
            var game = dbContext.Games.Find(Id);
            if (game is not null)
            {
                dbContext.Games.Remove(game);
                dbContext.SaveChanges();
                return Results.NoContent();
            }
            return Results.NoContent();
        }).WithName(DeleteGameEndpointName);

        return group;
    }
}
