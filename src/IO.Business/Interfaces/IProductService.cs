using System;
using System.Threading.Tasks;
using IO.Business.Models;

namespace IO.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);

        Task Update(Product product);

        Task Remove(Guid productId);
    }
}
