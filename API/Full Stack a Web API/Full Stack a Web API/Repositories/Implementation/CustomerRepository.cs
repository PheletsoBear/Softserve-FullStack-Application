using Full_Stack_a_Web_API.Data;
using Full_Stack_a_Web_API.Models.Domain;
using Full_Stack_a_Web_API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Full_Stack_a_Web_API.Repositories.Implementation
{
    public class CustomerRepository: ICustomerRespository
    {
        private readonly CustomerDbContext DbContext;

        public CustomerRepository(CustomerDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        // Creating Customer
        public async Task<Customer> CreateAsync(Customer customer)
        {
            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();
            return customer;

        }

        public async Task<Customer> DeleteAsync(Guid id)
        {
            var existingCustomer = await DbContext.Customers.FirstOrDefaultAsync(x => x.CustomerID == id);

            if (existingCustomer is null)
            {
                return null;
            }
            DbContext.Customers.Remove(existingCustomer);
          await DbContext.SaveChangesAsync();
            return existingCustomer;
        }

        //Get all customers
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await DbContext.Customers.ToListAsync();
        }

        //GetById
        public async Task<Customer?> GetById ([FromRoute] Guid id)
        {
            var existingCustomer = await DbContext.Customers.FirstOrDefaultAsync(x => x.CustomerID == id);
            return existingCustomer;

        }

        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            var existingCustomer = await DbContext.Customers.FirstOrDefaultAsync(x => x.CustomerID == customer.CustomerID);


            if (existingCustomer != null)
            {
                DbContext.Entry(existingCustomer).CurrentValues.SetValues(customer);
                await DbContext.SaveChangesAsync(true);
                return customer;
            }
            else
            {
                return null;

            }

        }
    }
}
