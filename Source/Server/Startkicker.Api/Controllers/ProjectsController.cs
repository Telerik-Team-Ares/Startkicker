namespace Startkicker.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Startkicker.Api.Models.Request.Projects;
    using Startkicker.Api.Models.Response.Projects;
    using Startkicker.Data.Models;
    using Startkicker.Services.Data.Contracts;

    public class ProjectsController : ApiController
    {
        private readonly IProjectsService projects;

        public ProjectsController(IProjectsService projects)
        {
            this.projects = projects;
        }

        [HttpGet]
       // [Authorize]
        public IHttpActionResult GetById(int id)
        {
            Project projectDataModel = this.projects.GetById(id);
            ProjectDescriptionResponseModel result = new ProjectDescriptionResponseModel
            {
                CategoryName = projectDataModel.Category.Name,
                Name = projectDataModel.Name,
                CollectedMoney = projectDataModel.CollectedMoney,
                Contributors = projectDataModel.Contributors.Select(x => x.User.UserName).ToList<string>(),
                Description = projectDataModel.Description,
                EstimatedDate = projectDataModel.EstimatedDate,
                GoalMoney = projectDataModel.GoalMoney,
                //Innovator = projectDataModel.Innovator.UserName,
                IsClosed = projectDataModel.IsClosed,
            };

            return this.Ok(result);
        }

        [HttpPost]
        //[Authorize]
        public IHttpActionResult Add(NewProjectRequestModel projectModel)
        {
            this.projects.Add(new Project
            {
                EstimatedDate = DateTime.Now.AddDays(projectModel.EstimatedDays),
                Name = projectModel.Name,
                IsRemoved = false,
                IsClosed = false,
                CollectedMoney = 0,
                Description = projectModel.Description,
                GoalMoney = projectModel.GoalMoney,
                InnovatorId = this.User.Identity.GetUserId(),
                CategoryId = projectModel.CategoryId,
            });

            return this.Ok();
        }
    }
}