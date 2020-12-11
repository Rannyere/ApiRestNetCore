using System;
using System.Linq;
using System.Threading.Tasks;
using IO.Business.Interfaces;
using IO.Business.Models;
using IO.Business.Validations;

namespace IO.Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository,
                               IAddressRepository addressRepository,
                               INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }

        public async Task<bool> Add(Provider provider)
        { 
            if (!ExecuteValidation(new ProviderValidation(), provider)
                || !ExecuteValidation(new AddressValidation(), provider.Address)) return false;

            if (_providerRepository.Search(s => s.Document == provider.Document).Result.Any())
            {
                Notify("There is already a provider with this information document.");
                return false;
            }

            await _providerRepository.Add(provider);
            return true;
        }

        public async Task<bool> Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider)) return false;

            if(_providerRepository.Search(s => s.Document == provider.Document && s.Id != provider.Id).Result.Any())
            {
                Notify("There is already a provider with this information document.");
                return false;
            }

            await _providerRepository.Update(provider);
            return true;
        }

        public async Task<bool> UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new  AddressValidation(), address)) return false;

            await _addressRepository.Update(address);
            return true;
        }

        public async Task<bool> Remove(Guid providerId)
        {
            if (_providerRepository.GetProviderProductsAddress(providerId).Result.Products.Any())
            {
                Notify("the supplier has registered products.");
                return false;
            }

            var address = await _addressRepository.GetAddressByProvider(providerId);

            if (address != null)
            {
                await _addressRepository.Remove(address.Id);
            }

            await _providerRepository.Remove(providerId);
            return true;
        }

        public void Dispose()
        {
            _providerRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
