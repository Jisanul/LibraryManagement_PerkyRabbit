namespace LibraryManagementRazorClient.ViewModels
{
    public class Member
    {
        public int MemberID { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
