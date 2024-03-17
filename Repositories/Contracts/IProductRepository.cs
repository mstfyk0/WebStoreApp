using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;

namespace Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IQueryable<Product> GetAllProducts (bool trackChange);
        IQueryable<Product> GetAllProductsWithDetails (ProductRequestParameters p);

        IQueryable<Product> GetShowCaseProducts (bool trackChange);

        Product? GetOneProduct(int id, bool trackChange);
        void CreateOneProduct (Product product);
        void DeleteOneProduct(Product product);
        void UpdateOneProduct(Product entity);
    }
}