namespace LibraryManagementSystemWPFClient.ViewModels
{
	public class BorrowedBook
	{
		public int BorrowedBookID { get; set; }
		public DateTime BorrowDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public int MemberID { get; set; }
		public string MemberName => Member.FullName;
		public Member Member { get; set; }
		public int BookID { get; set; }
		public Book Book { get; set; }
		public string BookName => Book.Title;
		public string Status { get; set; }
	}
}
