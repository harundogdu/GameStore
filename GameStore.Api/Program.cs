using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
app.MapGet("games",() => games);

// GET: Get a game by id
app.MapGet("games/{id}", (int id) => {
    return games.Find(item => item.Id == id);
});

// GET: Get Main page
app.MapGet("/", () => "Hello from GameStore.Api");

app.Run();
