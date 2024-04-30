namespace LibraryManagementSystem.Models
{
    public class BorrowedBook
    {
        public int BorrowedBookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public string Status { get; set; }
        public virtual Book Book { get; set; }
    }
}
