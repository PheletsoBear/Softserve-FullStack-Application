using Full_Stack_a_Web_API.Models.Domain;

namespace Full_Stack_a_Web_API.Repositories.Interface
{
    public interface ICustomerRespository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer?> GetById(Guid id);

        Task<Customer?> UpdateAsync(Customer customer);
        Task<Customer> DeleteAsync(Guid id);

    }
}
