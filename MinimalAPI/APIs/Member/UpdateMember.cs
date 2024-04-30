using LibraryManagementSystem.API_Setup;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.APIs.Member
{
    public class UpdateMember : IWebApi
    {
        public void Register(WebApplication app)
        {
            app.MapPut("/UpdateMember", async ([FromBody] Models.Member member, [FromServices] IMemberRepository memberRepository) =>
            {
                if (member != null)
                {
                    await memberRepository.UpdateMember(member);
                    return Results.Ok();

                }
                return Results.NoContent();
            })
        .WithMetadata(new EndpointNameMetadata("UpdateMember"));
        }
    }


}
