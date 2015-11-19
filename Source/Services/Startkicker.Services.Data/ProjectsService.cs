﻿namespace Startkicker.Services.Data
{
    using System;
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

        public Project GetById(int id)
        {
            Project result = this.projectsRepo.GetById(id);
            if (result != null && !result.IsRemoved)
            {
                return result;
            }

            return null;
        }

        public IQueryable<Project> GetAll(int page = 1, int pageSize = 10)
        {
            return this.projectsRepo
                .All()
                .OrderByDescending(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Where(x=> x.IsRemoved == false);
        }

        //public void Add(Project project)
        //{
        //    this.projectsRepo.Add(project);
        //    this.projectsRepo.SaveChanges();
        //}
        // Fix the interface also ! 

        public int Add(Project project)
        {
            this.projectsRepo.Add(project);
            this.projectsRepo.SaveChanges();

            return project.Id;
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
                //return -1;

                throw new UnauthorizedAccessException("User could not be found");
            }

            if ((user.MoneyAmount - amount) <= 0)
            {
                return 0;
            }

            Project projectToUpdate = this.projectsRepo.GetById(projectId);

            if (projectToUpdate == null)
            {
                throw new ArgumentOutOfRangeException("Could not find the project. Make sure the ID is correct!");
               // return -1;
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
