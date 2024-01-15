namespace Full_Stack_a_Web_API.Models.Domain
{
    public class Customer
    {

        public Guid CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public int DateOfBirth { get; set; }
        public string? Age { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public bool IsDeleted { get; set; }

    }
}
