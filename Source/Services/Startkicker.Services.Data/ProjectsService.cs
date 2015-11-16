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

        public ProjectsService(IRepository<Project> projectsRepo)
        {
            this.projectsPepo = projectsRepo;
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
    }
}
