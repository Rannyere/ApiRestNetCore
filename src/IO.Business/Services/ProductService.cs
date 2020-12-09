using System;
using System.Threading.Tasks;
using IO.Business.Interfaces;
using IO.Business.Models;
using IO.Business.Validations;

namespace IO.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
                              INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(product);
        }

        public async Task Remove(Guid productId)
        {
            await _productRepository.Remove(productId);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
