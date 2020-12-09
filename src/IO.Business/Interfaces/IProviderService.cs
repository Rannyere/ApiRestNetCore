using System;
using System.Threading.Tasks;
using IO.Business.Models;

namespace IO.Business.Interfaces
{
    public interface IProviderService : IDisposable
    {
        Task Add(Provider provider);

        Task Update(Provider provider);

        Task Remove(Guid providerId);

        Task UpdateAddress(Address address);
    }
}
