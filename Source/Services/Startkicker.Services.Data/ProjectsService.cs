namespace Startkicker.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Startkicker.Data.Models;
    using Startkicker.Data.Repositories;
    using Startkicker.Services.Data.Contracts;

    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<Project> projectsPepo;

        private readonly IRepository<User> usersRepo;

        public ProjectsService(IRepository<Project> projectsRepo, IRepository<User> usersRepo)
        {
            this.projectsPepo = projectsRepo;
            this.usersRepo = usersRepo;
        }

        public Project GetById(int id)
        {
            Project result = this.projectsPepo.GetById(id);
            if (result != null && !result.IsRemoved)
            {
                return result;
            }

            return null;
        }

        public IQueryable<Project> GetAll(int page = 1, int pageSize = 10)
        {
            return this.projectsPepo
                .All()
                .OrderByDescending(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public void Add(Project project)
        {
            this.projectsPepo.Add(project);
            this.projectsPepo.SaveChanges();
        }

        public void Update(Project project)
        {
            this.projectsPepo.Update(project);
            this.projectsPepo.SaveChanges();
        }

        public void Remove(Project project)
        {
            project.IsRemoved = true;
            this.projectsPepo.Update(project);
            this.projectsPepo.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Project projectToRemove = this.projectsPepo.GetById(id);
            projectToRemove.IsRemoved = true;
            this.projectsPepo.Update(projectToRemove);
            this.projectsPepo.SaveChanges();
        }

        public int AddMoney(int projectId, int amount, string userId)
        {
            User user = this.usersRepo.GetById(userId);

            if (user == null)
            {
                return -1;
            }

            if ((user.MoneyAmount - amount) < 0)
            {
                return 0;
            }

            Project projectToUpdate = this.projectsPepo.GetById(projectId);

            if (projectToUpdate == null)
            {
                return -1;
            }

            projectToUpdate.CollectedMoney += amount;
            this.projectsPepo.Update(projectToUpdate);

            user.MoneyAmount -= amount;
            this.usersRepo.Update(user);

            this.projectsPepo.SaveChanges();
            this.usersRepo.SaveChanges();

            return 1;
        }
    }
}
