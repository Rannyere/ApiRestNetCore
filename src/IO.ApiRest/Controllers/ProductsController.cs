using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using IO.ApiRest.DTOs;
using IO.Business.Interfaces;
using IO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static IO.ApiRest.Extensions.CustomAuthorize;

namespace IO.ApiRest.Controllers
{
    [Authorize]
    [Route("api/products")]
    public class ProductsController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,
                                  IProductService proctService,
                                  INotifier notifier,
                                  IMapper mapper) : base(notifier)
        {
            _productRepository = productRepository;
            _productService = proctService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsProviders());

        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetProductById(Guid id)
        {
            var productViewModel = await GetSpecificProduct(id);

            if (productViewModel == null) return NotFound();

            await _productService.Remove(id);

            return productViewModel;
        }

        [ClaimsAuthorize("Product","Add")]
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> AddProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = Guid.NewGuid() + "_" + productViewModel.Image;

            if (!UploadFile(productViewModel.ImageUpload, imageName)) return CustomResponse(productViewModel);

            productViewModel.Image = imageName;

            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return CustomResponse(productViewModel);
        }

        [ClaimsAuthorize("Product", "Add")]
        [RequestSizeLimit(40000000)]
        //[DisableRequestSizeLimit]
        [HttpPost("large-image")]
        public async Task<ActionResult<ProductViewModel>> AddProductWithIFormFile(ProductImageViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imgPrefix = Guid.NewGuid() + "_";
            if (!await UploadIFormFile(productViewModel.ImageUpload, imgPrefix))
            {
                return CustomResponse(ModelState);
            }

            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;
            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return CustomResponse(productViewModel);
        }

        [ClaimsAuthorize("Product", "Update")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                NotifierError("The IDs entered are not the same!");
                return CustomResponse();
            }

            var productUpdate = await GetSpecificProduct(id);

            if (string.IsNullOrEmpty(productViewModel.Image))
                productViewModel.Image = productUpdate.Image;

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (productViewModel.ImageUpload != null)
            {
                var imageName = Guid.NewGuid() + "_" + productViewModel.Image;
                if (!UploadFile(productViewModel.ImageUpload, imageName))
                {
                    return CustomResponse(ModelState);
                }

                productUpdate.Image = imageName;
            }

            productUpdate.ProviderId = productViewModel.ProviderId;
            productUpdate.Name = productViewModel.Name;
            productUpdate.Description = productViewModel.Description;
            productUpdate.Value = productViewModel.Value;
            productUpdate.Activ = productViewModel.Activ;

            await _productService.Update(_mapper.Map<Product>(productUpdate));

            return CustomResponse(productViewModel);
        }

        [ClaimsAuthorize("Product", "Delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(Guid id)
        {
            var product = await GetSpecificProduct(id);

            if (product == null) return NotFound();

            return CustomResponse(product);
        }       

        private async Task<ProductViewModel> GetSpecificProduct(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetProductProvider(id));
        }

        private bool UploadFile(string arquivo, string imgName)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotifierError("Upload an image for this product!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgName);

            if (System.IO.File.Exists(filePath))
            {
                NotifierError("A file with this name already exists!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }

        private async Task<bool> UploadIFormFile(IFormFile file, string imgPrefix)
        {
            if (file == null || file.Length == 0)
            {
                NotifierError("Upload an image for this product!");
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app/demo-webapi/src/assets", imgPrefix + file.FileName);

            if (System.IO.File.Exists(path))
            {
                NotifierError("A file with this name already exists!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }
    }
}
