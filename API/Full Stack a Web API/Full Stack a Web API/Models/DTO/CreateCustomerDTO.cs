using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Full_Stack_a_Web_API.Models.DTO
{
    public class CreateCustomerDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [JsonIgnore] // This ignores the property of From the JSON file 
        public string? UserName { get; set; } //This Property is will not be populated as input but will result as an output from Concatenation of FirstName and LastName

        [EmailAddress(ErrorMessage ="Invalid email address format")] // This sets up the email validation
        public string? EmailAddress { get; set; }

        [RegularExpression(@"^((19|20)\d{2})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[0-1])$", ErrorMessage ="Date of Birth must be in 'YYYY-MM-DD' format")]
        public string? DateOfBirth { get; set; }

        [JsonIgnore]// This ignores the property of From the JSON file 
        public int? Age { get; set; } //This Property is will not be populated as input but will result as an output from the calculation of the birthday

        [JsonIgnore]
        public DateTime DateCreated { get; set; }

        //This property will be ignored and it's initial value will be set as null and will only be set when it is edited in the PUT method/JSON
        [JsonIgnore]
        public DateTime DateEdited { get; set; }
        public bool IsDeleted { get; set; }



    }
}
