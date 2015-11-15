namespace Startkicker.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Startkicker.Api.Infrastructure.ActionFilters;
    using Startkicker.Api.Infrastructure.Helpers;
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
        //[Authorize]
        [EncryptResultIds]
        [DecryptInputId]
        public IHttpActionResult GetById(string id)
        {
            int idTo = int.Parse(id);
            Project projectDataModel = this.projects.GetById(idTo);
            if (projectDataModel != null)
            {
                ProjectDescriptionResponseModel result = new ProjectDescriptionResponseModel
                {
                    CategoryName = projectDataModel.Category.Name,
                    Name = projectDataModel.Name,
                    CollectedMoney = projectDataModel.CollectedMoney,
                    Contributors = projectDataModel.Contributors.Select(x => x.User.UserName).ToList<string>(),
                    Description = projectDataModel.Description,
                    EstimatedDate = projectDataModel.EstimatedDate,
                    GoalMoney = projectDataModel.GoalMoney,
                    InnovatorId = "2",
                    //Innovator = projectDataModel.Innovator.UserName,
                    IsClosed = projectDataModel.IsClosed,
                };
              
                return this.Ok(result);
            }

            return this.BadRequest("Project was not found!");
        }

        [HttpPost]
        [ValidateModelState]
        [CheckModelForNull]
        [Authorize]
        public IHttpActionResult Add(NewProjectRequestModel projectModel)
        {
            this.projects.Add(
                new Project
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