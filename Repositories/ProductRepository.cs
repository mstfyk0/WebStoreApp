using System.Security.Cryptography.X509Certificates;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Repositories.Extensions;

namespace Repositories
{
    public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {


        }

        public void CreateOneProduct(Product product) => Create(product);

        public void DeleteOneProduct(Product product) => Remove(product);

        public IQueryable<Product> GetAllProducts(bool trackChange) => FindAll(trackChange);

        public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            // return p.CategoryId is null
            // ? _context
            //     .Products
            //     .Include(prd=> prd.Category)
            // :_context
            //     .Products
            //     .Include(prd=> prd.Category)
            //     .Where(prd => prd.CategoryId.Equals(p.CategoryId));
            return _context
                .Products
                .FilterByCategoryId(p.CategoryId)
                .FilteredBySearchTerm(p.SearchTerm)
                .FilteredByPrice(p.MinPrice, p.MaxPrice, p.IsValidPrice)
                .ToPaginate(p.PageNumber,p.PageSize)
                ;
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            return FindByCondition(p => p.ProductId.Equals(id), trackChanges);

        }

        public IQueryable<Product> GetShowCaseProducts(bool trackChange)
        {
            return FindAll(trackChange)
            .Where(p => p.ShowCase.Equals(true));
        }

        public void UpdateOneProduct(Product entity) => Update(entity);
    }


}