using System;
using System.Threading.Tasks;
using IO.Business.Interfaces;
using IO.Business.Models;
using IO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IO.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ControlDbContext context) : base(context) { }

        public async Task<Address> GetAddressByProvider(Guid providerId)
        {
            return await Db.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(provider => provider.ProviderId == providerId);
        }
    }
}
