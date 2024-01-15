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
        public async Task<IActionResult> CreateCustomer(CustomerDTO request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //mapping DTO to Domain model

            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailAddress = request.EmailAddress,
                DateOfBirth = request.DateOfBirth,
                Age = request.Age,
                DateCreated = request.DateCreated,
                DateEdited = request.DateEdited,
                IsDeleted = request.IsDeleted
            };

            await customerRespository.CreateAsync(customer);

            //map Domain model to   DTO

            var response = new CustomerDTO
            {
                CustomerID = customer.CustomerID,
                FirstName = request.FirstName,
                UserName = request.LastName,
                EmailAddress = request.EmailAddress,
                DateOfBirth = request.DateOfBirth,
                Age = request.Age,
                DateCreated = request.DateCreated,
                DateEdited = request.DateEdited,
                IsDeleted = request.IsDeleted
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

            if(existingCategory is null)
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
            //map Domain model to DTO
            var customer = new Customer
            {
                CustomerID = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailAddress = request.EmailAddress,
                DateOfBirth = request.DateOfBirth,
                Age = request.Age,
                DateCreated = request.DateCreated,
                DateEdited = request.DateEdited,
                IsDeleted = request.IsDeleted

            };

            customer = await customerRespository.UpdateAsync(customer);

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

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var customer = await customerRespository.DeleteAsync(id);

            if(customer == null)
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




