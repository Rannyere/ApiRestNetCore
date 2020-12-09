using System;
using System.Threading.Tasks;
using IO.Business.Models;

namespace IO.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByProvider(Guid providerId);
    }
}
