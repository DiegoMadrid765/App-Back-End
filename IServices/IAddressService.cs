using Back_End.Models;

namespace Back_End.IServices
{
    public interface IAddressService
    {
        Task AddAddress(Address address);
    }
}
