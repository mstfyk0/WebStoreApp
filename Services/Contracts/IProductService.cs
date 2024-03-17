using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProduct(bool trackChnages);
        IEnumerable<Product> GetLastestProduct( int n ,bool trackChnages);
        IEnumerable<Product> GetAllProductsWithDetails (ProductRequestParameters p);
        IEnumerable<Product> GetShowCaseProducts(bool trackChange);

        Product? GetOneProduct(int id, bool trackChnages);

        void CreateProduct(ProductDtoForInsertion productDto);
        void UpdateOneProduct(ProductDtoForUpdate product);
        void DeleteOneProduct(int id);
        ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChnages);
    }
}