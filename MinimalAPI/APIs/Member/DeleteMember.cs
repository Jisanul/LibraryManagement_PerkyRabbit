using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Member
{
    public class DeleteMember : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapDelete("/DeleteMember/{id}", async (int id,
                                            [FromServices] IMemberRepository memberRepository,
                                            [FromServices] LinkGenerator linkGenerator) =>
            {

                await memberRepository.DeleteMember(id);
                return Results.Ok();
            })
            .WithMetadata(new EndpointNameMetadata("DeleteMember"));
        }
    }

}
