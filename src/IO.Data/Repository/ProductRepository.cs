using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IO.Business.Interfaces;
using IO.Business.Models;
using IO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IO.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ControlDbContext context) : base(context) { }

        public async Task<Product> GetProductProvider(Guid productId)
        {
            return await Db.Products.AsNoTracking()
                .Include(provider => provider.Provider)
                .FirstOrDefaultAsync(product => product.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId)
        {
            return await Search(product => product.ProviderId == providerId);
        }

        public async Task<IEnumerable<Product>> GetProductsProviders()
        {
            return await Db.Products.AsNoTracking()
                .Include(provider => provider.Provider)
                .OrderBy(Provider => Provider.Name).ToListAsync();
        }
    }
}
