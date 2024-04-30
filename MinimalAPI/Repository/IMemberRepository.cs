using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repository
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();

        Task<Member> GetMemberByID(int memberID);

        Task InsertMember(Member member);

        Task UpdateMember(Member member);

        void Save();
        Task SaveAsync();
        Task DeleteMember(int member);
    }
}
