using LibraryManagementSystem.DBContexts;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class MemberRepository : IMemberRepository
    {


        private readonly LibraryDBContext _dbContext;

        public MemberRepository(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            return await _dbContext.Members.ToListAsync();
        }

        public async Task<Member> GetMemberByID(int memberId)
        {
            return await _dbContext.Members.FirstOrDefaultAsync(a => a.MemberID == memberId);
        }

        public async Task InsertMember(Member member)
        {
            await _dbContext.AddAsync(member);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateMember(Member member)
        {
            _dbContext.Entry(member).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteMember(int memberId)
        {
            var member = await _dbContext.Members.FindAsync(memberId);
            if (member != null)
            {
                _dbContext.Members.Remove(member);
                await _dbContext.SaveChangesAsync();
            }
        }

    }

}
