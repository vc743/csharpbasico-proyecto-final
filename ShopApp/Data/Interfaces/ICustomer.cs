using ShopApp.Data.Dtos;

namespace ShopApp.Data.Interfaces
{
    public interface ICustomer
    {
        void SaveCustomer(CustomerAddDto addDto);
        void RemoveCustomer(CustomerRemoveDto removeDto);
        void UpdateCustomer(CustomerUpdateDto updateDto);
        List<CustomerAddDto> GetCustomers();
        CustomerAddDto GetCustomerById(int customerId);
    }
}
