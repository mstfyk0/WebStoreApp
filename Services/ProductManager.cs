using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public ProductManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            // Product product = new Product(){
            //     ProductName = productDto.ProductName,
            //     Price = productDto.Price,
            //     CategoryId= productDto.CategoryId
            // };
            Product product = _mapper.Map<Product>(productDto);
            _manager.Product.Create(product);
            _manager.Save();
        }

        public void DeleteOneProduct(int id)
        {
            Product product = GetOneProduct(id, false) ?? new Product();
            _manager.Product.DeleteOneProduct(product);
            _manager.Save();
        }

        public IEnumerable<Product> GetAllProduct(bool trackChnages)
        {
            // throw new NotImplementedException();
            return _manager.Product.GetAllProducts(trackChnages);
        }

        public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            return _manager.Product.GetAllProductsWithDetails(p);
        }

        public IEnumerable<Product> GetLastestProduct(int n, bool trackChnages)
        {
            return _manager.Product.FindAll(trackChnages)
            .OrderByDescending(prd => prd.ProductId)
            .Take(n);
        }

        public Product? GetOneProduct(int id, bool trackChnages)
        {

            var product = _manager.Product.GetOneProduct(id, trackChnages);
            if (product is null)
            {

                throw new Exception("Product Not Found!");

            }
            return product;
        }

        public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChnages)
        {
            var product = GetOneProduct(id, trackChnages);
            var productDto = _mapper.Map<ProductDtoForUpdate>(product);
            return productDto;
        }

        public IEnumerable<Product> GetShowCaseProducts(bool trackChange)
        {
           var product = _manager.Product.GetShowCaseProducts(trackChange);
           return product;
        }

        public void UpdateOneProduct(ProductDtoForUpdate productDto)
        {
            // var entity = _manager.Product.GetOneProduct(productDto.ProductId, true);
            // entity.ProductName = productDto.ProductName;
            // entity.Price = productDto.Price;
            // entity.CategoryId=productDto.CategoryId;
            var entity = _mapper.Map<Product>(productDto);
            _manager.Product.UpdateOneProduct(entity);
            _manager.Save();

        }
    }
}