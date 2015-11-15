namespace Startkicker.Services.Data
{
    using System;
    using System.Collections.Generic;

    using Startkicker.Data.Models;
    using Startkicker.Data.Repositories;
    using Startkicker.Services.Data.Contracts;
    using System.Linq;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesPepo;

        public CategoriesService(IRepository<Category> categoriesRepo)
        {
            this.categoriesPepo = categoriesRepo;
        }

        public Category GetById(int id)
        {
            Category result = this.categoriesPepo.GetById(id);
            if (result != null && !result.IsRemoved)
            {
                return result;
            }

            return null;
        }

        public IQueryable<Category> GetAll(int page=1,int pageSize = 10)
        {
            return this.categoriesPepo
                .All()
                .OrderByDescending(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }


        public int Add(Category category)
        {
            if (this.categoriesPepo.All().Contains(category))
            {
                return -1;
            }

            this.categoriesPepo.Add(category);
            this.categoriesPepo.SaveChanges();
            return category.Id;
        }

        public int Update(Category category)
        {
            this.categoriesPepo.Update(category);
            this.categoriesPepo.SaveChanges();
            return category.Id;
        }

        public void Remove(Category Category)
        {
            Category.IsRemoved = true;
            this.categoriesPepo.Update(Category);
            this.categoriesPepo.SaveChanges();
        }
    }
}
