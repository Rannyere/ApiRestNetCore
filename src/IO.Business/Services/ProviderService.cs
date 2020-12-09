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

        public async Task Add(Provider provider)
        { 
            if (!ExecuteValidation(new ProviderValidation(), provider)
                || !ExecuteValidation(new AddressValidation(), provider.Address)) return;

            if (_providerRepository.Search(s => s.Document == provider.Document).Result.Any())
            {
                Notify("There is already a provider with this information document.");
                return;
            }

            await _providerRepository.Add(provider);
        }

        public async Task Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider)) return;

            if(_providerRepository.Search(s => s.Document == provider.Document && s.Id != provider.Id).Result.Any())
            {
                Notify("There is already a provider with this information document.");
                return;
            }

            await _providerRepository.Update(provider);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new  AddressValidation(), address)) return;

            await _addressRepository.Update(address);
        }

        public async Task Remove(Guid providerId)
        {
            if (_providerRepository.GetProviderProductsAddress(providerId).Result.Products.Any())
            {
                Notify("the supplier has registered products.");
                return;
            }

            var address = await _addressRepository.GetAddressByProvider(providerId);

            if (address != null)
            {
                await _addressRepository.Remove(address.Id);
            }

            await _providerRepository.Remove(providerId);
        }

        public void Dispose()
        {
            _providerRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
