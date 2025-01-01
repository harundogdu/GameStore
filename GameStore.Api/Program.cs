using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var application = builder.Build();

application.MapGamesEndpoints();

application.Run();
