using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Full_Stack_a_Web_API.Models.DTO
{
    public class UpdateEmployeeDTO
    {

      

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [JsonIgnore]
        public string? UserName { get; set; }
     
        [EmailAddress(ErrorMessage = "Invalid email address format")] // This sets up the email validation
        public string? EmailAddress { get; set; }

        [RegularExpression(@"^((19|20)\d{2})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Date of Birth must be in 'YYYY-MM-DD' format")]
        public string? DateOfBirth { get; set; }

        [JsonIgnore]// This ignores the property of From the JSON file
        public int? Age { get; set; }

        [JsonIgnore]
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public DateTime DateEdited { get; set; }
        public bool IsDeleted { get; set; }

    }
}