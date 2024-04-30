namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        public string ISBN { get; set; }
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
    }
}
