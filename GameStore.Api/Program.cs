using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var application = builder.Build();

application.MapGamesEndpoints();
application.MapGenresEndpoints();

await application.MigrateDbAsync();

application.Run();
