using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IO.ApiRest.DTOs;
using IO.Business.Interfaces;
using IO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace IO.ApiRest.Controllers
{
    [Route("api/providers")]
    public class ProvidersController : MainController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProviderViewModel>>> SearchAll()
        {
            var providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.SearchAll());

            return Ok(providers);
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
            if (!ModelState.IsValid) return BadRequest();

            var provider = _mapper.Map<Provider>(providerViewModel);
            var result = await _providerService.Add(provider);

            if (!result) return BadRequest();

            return Ok(provider);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProviderViewModel>> Update(Guid id, ProviderViewModel providerViewModel)
        {
            if (id != providerViewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var provider = _mapper.Map<Provider>(providerViewModel);
            var result = await _providerService.Update(provider);

            if (!result) return BadRequest();

            return Ok(provider);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProviderViewModel>> Delete(Guid id)
        {
            var provider = await GetProviderAddress(id);

            if (provider == null) return NotFound();

            var result = await _providerService.Remove(id);

            if (!result) return BadRequest();

            return Ok(provider);
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
