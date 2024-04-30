using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Member
{
    public class GetMemberByID : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/GetMemberByID", async (int id, [FromServices] IMemberRepository memberRepository) =>
            {
                var person = await memberRepository.GetMemberByID(id);
                return Results.Ok(person);
            })
        .WithMetadata(new EndpointNameMetadata("GetMemberByID"));
        }
    }

}
