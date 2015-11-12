namespace Startkicker.Services.Data.Contracts
{
    using System.Collections.Generic;

    using Startkicker.Data.Models;

    public interface IProjectsService
    {
        Project GetById(int id);

        void Add(Project project);

        void Update(Project project);

        void Remove(Project project);
    }
}
