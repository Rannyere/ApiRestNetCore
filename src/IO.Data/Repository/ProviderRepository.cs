using System;
using System.Threading.Tasks;
using IO.Business.Interfaces;
using IO.Business.Models;
using IO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IO.Data.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(ControlDbContext context) : base(context) { }

        public async Task<Provider> GetProviderAddress(Guid providerId)
        {
            return await Db.Providers.AsNoTracking()
                .Include(address => address.Address)
                .FirstOrDefaultAsync(provider => provider.Id == providerId);
        }

        public async Task<Provider> GetProviderProductsAddress(Guid providerId)
        {
            return await Db.Providers.AsNoTracking()
                .Include(address => address.Address)
                .Include(product => product.Products)
                .FirstOrDefaultAsync(provider => provider.Id == providerId);
        }
    }
}
