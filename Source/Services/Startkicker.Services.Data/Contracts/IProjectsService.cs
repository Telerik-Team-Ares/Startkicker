namespace Startkicker.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Startkicker.Data.Models;

    public interface IProjectsService
    {
        Project GetById(int id);

        IQueryable<Project> GetAll(int page = 1, int pageSize = 10);

        void Add(Project project);

        int AddMoney(int projectId, int amount, string userId);

        void Update(Project project);

        void Remove(Project project);
    }
}
