namespace CleanArchitecture.Api.Extensions;

public static class EndpointGroups
{
    extension(WebApplication app)
    {
        public RouteGroupBuilder Players()
        {
            return app.MapGroup("/players")
                .RequireAuthorization();
        }
    }
}
