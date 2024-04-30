namespace LibraryManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Password { get; set; }
        public virtual Member Member { get; set; }
    }
}
