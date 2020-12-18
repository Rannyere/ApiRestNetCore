using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IO.ApiRest.DTOs;
using IO.Business.Interfaces;
using IO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IO.ApiRest.Controllers
{
    [Authorize]
    [Route("api/providers")]
    public class ProvidersController : MainController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderService _providerService;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository,
                                   IProviderService providerService,
                                   IAddressRepository addressRepository,
                                   IMapper mapper,
                                   INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _providerService = providerService;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ProviderViewModel>> SearchAll()
        {
            return _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.SearchAll());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProviderViewModel>> Details(Guid id)
        {
            var provider = await GetProviderProductsAddress(id);

            if (provider == null) return NotFound();

            return provider;
        }

        [HttpPost]
        public async Task<ActionResult<ProviderViewModel>> Create(ProviderViewModel providerViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _providerService.Add(_mapper.Map<Provider>(providerViewModel));

            return CustomResponse(providerViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProviderViewModel>> Update(Guid id, ProviderViewModel providerViewModel)
        {
            if (id != providerViewModel.Id)
            {
                NotifierError("The given id is different from the id passed in the query");
                return CustomResponse(providerViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(providerViewModel);

            await _providerService.Update(_mapper.Map<Provider>(providerViewModel));        

            return CustomResponse(providerViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProviderViewModel>> Delete(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null) return NotFound();

            await _providerService.Remove(id);

            return CustomResponse(providerViewModel);
        }

        [HttpGet("get-address/{id:guid}")]
        public async Task<AddressViewModel> GetAddressById(Guid id)
        {
            return _mapper.Map<AddressViewModel>(await _addressRepository.GetAddressByProvider(id));
        }

        [HttpPut("update-address/{id:guid}")]
        public async Task<ActionResult> UpdateAddress(Guid id, AddressViewModel addressViewModel)
        {
            if (id != addressViewModel.Id)
            {
                NotifierError("The given id is different from the id passed in the query");
                return CustomResponse(addressViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _providerService.UpdateAddress(_mapper.Map<Address>(addressViewModel));

            return CustomResponse(addressViewModel);
        }

        private async Task<ProviderViewModel> GetProviderProductsAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderProductsAddress(id));
        }

        private async Task<ProviderViewModel> GetProviderAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderAddress(id));
        }
    }
}
