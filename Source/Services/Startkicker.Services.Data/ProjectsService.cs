namespace Startkicker.Services.Data
{
    using System.Linq;

    using Startkicker.Data.Models;
    using Startkicker.Data.Repositories;
    using Startkicker.Services.Data.Contracts;
    using System.Collections.Generic;
    using System;

    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<Project> projectsPepo;
        private readonly IRepository<User> usersRepo;

        public ProjectsService(IRepository<Project> projectsRepo, IRepository<User> usersRepo)
        {
            this.projectsPepo = projectsRepo;
            this.usersRepo = usersRepo;
        }

        public IQueryable<Project> GetById(int id)
        {
            return this.projectsPepo.All().Where(pr => pr.Id == id);
        }

        public IQueryable<Project> GetAll(int page = 1, int pageSize = 10)
        {
            return this.projectsPepo
                .All()
                .Where(x => (!x.IsRemoved))
                .OrderByDescending(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public int Add(string name, string description, int goalMoney, int estimatedDays, int categoryId, string userId, ICollection<Image> images)
        {
            var projectToAdd = new Project
            {
                EstimatedDate = DateTime.Now.AddDays(estimatedDays),
                Name = name,
                Description = description,
                GoalMoney = goalMoney,
                CategoryId = categoryId,
                InnovatorId = userId,
                Images = images,
                IsRemoved = false,
                IsClosed = false,
                CollectedMoney = 0
            };

            this.projectsPepo.Add(projectToAdd);
            this.projectsPepo.SaveChanges();

            return projectToAdd.Id;
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
