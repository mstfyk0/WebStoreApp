using Entities.Models;

namespace Services.Contracts
{
    public interface ICategoryServices
    {
        IEnumerable<Category> GetAllCategories(bool trackChanges);
    }
}