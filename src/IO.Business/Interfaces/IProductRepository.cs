using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IO.Business.Models;

namespace IO.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductProvider(Guid id);

        Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId);

        Task<IEnumerable<Product>> GetProductsProviders();
    }
}
