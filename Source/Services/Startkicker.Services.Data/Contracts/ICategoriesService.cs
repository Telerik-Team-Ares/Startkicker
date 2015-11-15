namespace Startkicker.Services.Data.Contracts
{
    using System.Linq;
    using Startkicker.Data.Models;

    public interface ICategoriesService
    {
        int Add(Category category);
        IQueryable<Category> GetAll(int page = 1, int pageSize = 10);
        Category GetById(int id);
        void Remove(Category Category);
        int Update(Category category);
    }
}