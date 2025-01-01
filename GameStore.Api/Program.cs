using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetMainEndpointName = "GetMain";
const string GetGameEndpointName = "GetGame";
const string GetGamesEndpointName = "GetGames";
const string PostGameEndpointName = "PostName";
const string PutGameEndpointName = "PutGame";
const string DeleteGameEndpointName = "DeleteGame";

List<GameDto> games = [
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

// GET: Get all games
app.MapGet("games", () => Results.Ok(games)).WithName(GetGamesEndpointName);

// GET: Get a game by id
app.MapGet("games/{id}", (int id) =>
{
    var game = games.FirstOrDefault(game => game.Id == id);
    if (game is not null)
    {
        return Results.Ok(game);
    }

    return Results.NotFound();
}).WithName(GetGameEndpointName);

// POST: Create a new game
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
       games.Count + 1,
       newGame.Name,
       newGame.Genre,
       newGame.Price,
       newGame.ReleaseDate
   );

    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
}).WithName(PostGameEndpointName);

// PUT: Update game by id
app.MapPut("games/{id}", (int id, UpdateGameDto updateGame) =>
{
    var gameIndex = games.FindIndex(game => game.Id == id);
    if (gameIndex > -1)
    {
        games[gameIndex] = new GameDto(
            id,
            updateGame.Name,
            updateGame.Genre,
            updateGame.Price,
            updateGame.ReleaseDate
        );

        return Results.NoContent();
    }

    return Results.NotFound();
}).WithName(PutGameEndpointName);

// DELETE: Delete a game by id
app.MapDelete("games/{id}", (int Id) =>
{
    games.RemoveAll(game => game.Id == Id);
    return Results.NoContent();
}).WithName(DeleteGameEndpointName);

// GET: Get Main page
app.MapGet("/", () => "Hello from GameStore.Api").WithName(GetMainEndpointName);

app.Run();
