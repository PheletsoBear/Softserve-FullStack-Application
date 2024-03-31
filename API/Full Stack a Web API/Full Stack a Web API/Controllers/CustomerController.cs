using Full_Stack_a_Web_API.Models.Domain;
using Full_Stack_a_Web_API.Models.DTO;
using Full_Stack_a_Web_API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Full_Stack_a_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRespository customerRespository;

        public CustomerController(ICustomerRespository customerRespository)
        {
            this.customerRespository = customerRespository;
        }

        

        //Create Customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



          

            string UserName = $"{request.FirstName} {request.LastName}"; //This creates Username by Concatenation

            //This parses the Date of birth to Datetime datatype and also ensures that if incorrect date format is inserted, the correct BadRequest message is sent out
            DateTime dateOfBirth;
            if (!DateTime.TryParse(request.DateOfBirth, out dateOfBirth))
            {
                return BadRequest("Invalid DateOfBirth format");
            }

           
            //This code declares today variable then finds the difference between the todays and the date of birth

            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            // Check if the birthday has not passed yet this year
            if (dateOfBirth > today.AddYears(-age)){
                // If the birthday hasn't passed yet, decrement the age by 1
                age--;
            }

            // Condition that if dateOfBirth is greater than today's date then bad request is flagged with error response 400
            if (dateOfBirth > today)
            {
                return BadRequest("Invalid rquest: Date of Birth cannot be greater than today's Date");
            }

            //mapping DTO to Domain model

            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = UserName,
                EmailAddress = request.EmailAddress,
                DateOfBirth = request.DateOfBirth,
                Age = age,
                DateCreated = DateTime.Today,
                DateEdited = (DateTime)request.DateEdited,
                IsDeleted = request.IsDeleted
            };

            await customerRespository.CreateAsync(customer);

            //map Domain model to   DTO

            var response = new CustomerDTO
            {
                CustomerID = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserName = customer.UserName,
                EmailAddress = customer.EmailAddress,
                DateOfBirth = customer.DateOfBirth,
                Age = customer.Age,
                DateCreated = customer.DateCreated,
                DateEdited = customer.DateEdited,
                IsDeleted = customer.IsDeleted
            };
            return Ok(response);

        }
        //Implementing Get All Customers

        [HttpGet]

        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await customerRespository.GetAllAsync();

            //Map DomaIN Model to DTO

            var response = new List<CustomerDTO>();
            foreach (var customer in customers)
            {
                response.Add(new CustomerDTO
                {
                    CustomerID = customer.CustomerID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    UserName = customer.UserName,
                    EmailAddress = customer.EmailAddress,
                    DateOfBirth = customer.DateOfBirth,
                    Age = customer.Age,
                    DateCreated = customer.DateCreated,
                    DateEdited = customer.DateEdited,
                    IsDeleted = customer.IsDeleted

                });
            }
            return Ok(response);


        }


        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetCustomerById([FromRoute] Guid id)
        {
            var existingCategory = await customerRespository.GetById(id);

            if (existingCategory is null)
            {
                return NotFound();
            }

            // map Domain model to DTO

            var response = new CustomerDTO
            {
                CustomerID = existingCategory.CustomerID,
                FirstName = existingCategory.FirstName,
                LastName = existingCategory.LastName,
                UserName = existingCategory.UserName,
                EmailAddress = existingCategory.EmailAddress,
                DateOfBirth = existingCategory.DateOfBirth,
                Age = existingCategory.Age,
                DateCreated = existingCategory.DateCreated,
                DateEdited = existingCategory.DateEdited,
                IsDeleted = existingCategory.IsDeleted
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> EditCustomer([FromRoute] Guid id, UpdateEmployeeDTO request)
        {


            string UserName = $"{request.FirstName} {request.LastName}";

            //Retrieving the Users details then assign the dateCreated stored in database to the request dateCreated


            var ExistingCustomer = await customerRespository.GetById(id);
            if (ExistingCustomer is null)
            {
                return NotFound();
            }
            else
            {
                request.DateCreated = ExistingCustomer.DateCreated;
            }





            //This parses the Date of birth to Datetime datatype and also ensures that if incorrect date format is inserted, the correct BadRequest message is sent out
            DateTime dateOfBirth;
            if (!DateTime.TryParse(request.DateOfBirth, out dateOfBirth))
            {
                return BadRequest("Invalid DateOfBirth format");
            }


            //This code declares today variable then finds the difference between the todays and the date of birth

            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            // Check if the birthday has not passed yet this year
            if (dateOfBirth > today.AddYears(-age))
            {
                // If the birthday hasn't passed yet, decrement the age by 1
                age--;
            }

            // Condition that if dateOfBirth is greater than today's date then bad request is flagged with error response 400
            if (dateOfBirth > today)
            {
                return BadRequest("Invalid rquest: Date of Birth cannot be greater than today's Date");
            }

            //map Domain model to DTO
            var customer = new Customer
            {
                CustomerID = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = UserName,
                EmailAddress = request.EmailAddress,
                DateOfBirth = request.DateOfBirth,
                Age = age,
                DateCreated = request.DateCreated,
                DateEdited = DateTime.Today,
                IsDeleted = request.IsDeleted

            };

            customer = await customerRespository.UpdateAsync(customer);

            var response = new CustomerDTO
            {
                CustomerID = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserName = customer.UserName,
                EmailAddress = customer.EmailAddress,
                DateOfBirth = customer.DateOfBirth,
                Age = customer.Age,
                DateCreated = customer.DateCreated,
                DateEdited = customer.DateEdited,
                IsDeleted = customer.IsDeleted
            };
            return Ok(response);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            var customer = await customerRespository.DeleteAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            //map domain model to DTO

            var response = new CustomerDTO
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserName = customer.UserName,
                EmailAddress = customer.EmailAddress,
                DateOfBirth = customer.DateOfBirth,
                Age = customer.Age,
                DateCreated = customer.DateCreated,
                DateEdited = customer.DateEdited,
                IsDeleted = customer.IsDeleted
            };
            return Ok(response);
        }


    }
}




