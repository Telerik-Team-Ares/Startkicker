namespace Startkicker.Services.Data.Contracts
{
    using System.Linq;

    using Startkicker.Data.Models;
    using System.Collections.Generic;

    public interface IProjectsService
    {
        IQueryable<Project> GetById(int id);

        IQueryable<Project> GetAll();

        IQueryable<Project> GetByCategory(int categoryId);

        int Add(string name, string description, int goalMoney, int estimatedDays, int categoryId, string userId, ICollection<Image> images);

        int AddMoney(int projectId, int amount, string userId);

        void Update(Project project);

        void Remove(Project project);

        void RemoveById(int id);
    }
}
