namespace Full_Stack_a_Web_API.Models.DTO
{
    public class UpdateEmployeeDTO
    {

        public Guid CustomerID { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public bool IsDeleted { get; set; }

    }
}