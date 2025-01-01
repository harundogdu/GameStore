using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetMainEndpointName = "GetMain";
const string GetGameEndpointName = "GetGame";
const string GetGamesEndpointName = "GetGames";
const string PostGameEndpointName = "PostName";

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
app.MapGet("games", () => games).WithName(GetGamesEndpointName);

// GET: Get a game by id
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);

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

// GET: Get Main page
app.MapGet("/", () => "Hello from GameStore.Api").WithName(GetMainEndpointName);

app.Run();
