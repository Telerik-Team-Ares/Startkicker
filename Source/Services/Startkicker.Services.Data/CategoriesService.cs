namespace Startkicker.Services.Data
{
    using System.Linq;

    using Startkicker.Data.Models;
    using Startkicker.Data.Repositories;
    using Startkicker.Services.Data.Contracts;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepo;

        public CategoriesService(IRepository<Category> categoriesRepo)
        {
            this.categoriesRepo = categoriesRepo;
        }

        public Category GetById(int id)
        {
            Category result = this.categoriesRepo.GetById(id);
            if (result != null && !result.IsRemoved)
            {
                return result;
            }

            return null;
        }

        public IQueryable<Category> GetPage(int page = 1, int pageSize = 10)
        {
            return this.categoriesRepo
                .All()
                .OrderByDescending(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public IQueryable<Category> GetAll()
        {
            return this.categoriesRepo.All();
        }

        public int Add(Category category)
        {
            if (this.categoriesRepo.All().Any(c => c.Name == category.Name))
            {
                return -1;
            }

            this.categoriesRepo.Add(category);
            this.categoriesRepo.SaveChanges();
            return category.Id;
        }

        public int Update(Category category)
        {
            this.categoriesRepo.Update(category);
            this.categoriesRepo.SaveChanges();
            return category.Id;
        }

        public void Remove(Category Category)
        {
            Category.IsRemoved = true;
            this.categoriesRepo.Update(Category);
            this.categoriesRepo.SaveChanges();
        }
    }
}
