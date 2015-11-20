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
        private readonly IRepository<Project> projectsRepo;
        private readonly IRepository<User> usersRepo;

        public ProjectsService(IRepository<Project> projectsRepo, IRepository<User> usersRepo)
        {
            this.projectsRepo = projectsRepo;
            this.usersRepo = usersRepo;
        }

        public IQueryable<Project> GetById(int id)
        {
            return this.projectsRepo.All().Where(pr => pr.Id == id);
        }

        public IQueryable<Project> GetByCategory(string categoryName)
        {
            return
                this.projectsRepo.All()
                    .Where(x => (!x.IsRemoved) && x.Category.Name == categoryName)
                    .OrderByDescending(c => c.Name);
            //.Skip((page - 1) * pageSize)
            //.Take(pageSize)

        }

        public IQueryable<Project> GetAll()
        {
            return this.projectsRepo.All().Where(x => (!x.IsRemoved)).OrderByDescending(c => c.Name);

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

            this.projectsRepo.Add(projectToAdd);
            this.projectsRepo.SaveChanges();

            return projectToAdd.Id;
        }

        public void Update(Project project)
        {
            this.projectsRepo.Update(project);
            this.projectsRepo.SaveChanges();
        }

        public void Remove(Project project)
        {
            project.IsRemoved = true;
            this.projectsRepo.Update(project);
            this.projectsRepo.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Project projectToRemove = this.projectsRepo.GetById(id);
            projectToRemove.IsRemoved = true;
            this.projectsRepo.Update(projectToRemove);
            this.projectsRepo.SaveChanges();
        }

        public int AddMoney(int projectId, int amount, string userId)
        {
            User user = this.usersRepo.GetById(userId);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User could not be found!");
            }

            if ((user.MoneyAmount - amount) <= 0)
            {
                return 0;
            }

            Project projectToUpdate = this.projectsRepo.GetById(projectId);

            if (projectToUpdate == null)
            {
                throw new ArgumentOutOfRangeException("Could not find the project. Make sure the ID is correct!");
            }

            if (projectToUpdate.InnovatorId == userId)
            {
                throw new UnauthorizedAccessException("User could not fund own project!");
            }

            projectToUpdate.CollectedMoney += amount;
            this.projectsRepo.Update(projectToUpdate);

            user.MoneyAmount -= amount;
            this.usersRepo.Update(user);

            this.projectsRepo.SaveChanges();
            this.usersRepo.SaveChanges();

            return 1;
        }
    }
}
