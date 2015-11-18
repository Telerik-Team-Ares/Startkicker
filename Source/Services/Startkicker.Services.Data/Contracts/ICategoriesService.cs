namespace Startkicker.Services.Data.Contracts
{
    using System.Linq;
    using Startkicker.Data.Models;

    public interface ICategoriesService
    {
        int Add(Category category);

        IQueryable<Category> GetPage(int page = 1, int pageSize = 10);

        Category GetById(int id);

        IQueryable<Category> GetAll();

        void Remove(Category category);

        int Update(Category category);
    }
}