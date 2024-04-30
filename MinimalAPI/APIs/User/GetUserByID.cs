using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.User
{
    public class GetUserByID : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/GetUserByID", async (int id, [FromServices] IUserRepository userRepository) =>
            {
                var person = await userRepository.GetUserByID(id);
                return Results.Ok(person);
            })
        .WithMetadata(new EndpointNameMetadata("GetUserByID"));
        }
    }

}
