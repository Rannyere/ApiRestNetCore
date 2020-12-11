using System;
using System.Threading.Tasks;
using IO.Business.Models;

namespace IO.Business.Interfaces
{
    public interface IProviderService : IDisposable
    {
        Task<bool> Add(Provider provider);

        Task<bool> Update(Provider provider);

        Task<bool> Remove(Guid providerId);

        Task<bool> UpdateAddress(Address address);
    }
}
