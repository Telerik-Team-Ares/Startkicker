namespace Startkicker.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Ninject.Infrastructure.Language;
    using Microsoft.AspNet.Identity;

    using Startkicker.Api.Common.Contracts;
    using Startkicker.Api.Infrastructure.ActionFilters;
    using Startkicker.Api.Infrastructure.Helpers;
    using Startkicker.Api.Models.Request.Projects;
    using Startkicker.Api.Models.Response.Projects;
    using Startkicker.Data.Models;
    using Startkicker.Services.Data.Contracts;

    using WebGrease.Css.Extensions;

    [RoutePrefix("api/Projects")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectsService projects;
        private readonly IPublisher publisher;

        public ProjectsController(IProjectsService projects, IPublisher publisher)
        {
            this.projects = projects;
            this.publisher = publisher;
        }

        [Route("GetById")]
        [HttpGet]
        //[Authorize]
        [EncryptResultIds]
        [DecryptInputId]
        //[Route("projects/getById/{id}")]
        public IHttpActionResult GetById(string id)
        {
            int idTo = int.Parse(id);
            Project projectDataModel = this.projects.GetById(idTo);
            if (projectDataModel != null)
            {
                ProjectDescriptionResponseModel result = new ProjectDescriptionResponseModel
                {
                    CategoryName =
                                                                     projectDataModel
                                                                     .Category.Name,
                    Name =
                                                                     projectDataModel
                                                                     .Name,
                    CollectedMoney =
                                                                     projectDataModel
                                                                     .CollectedMoney,
                    Contributors =
                                                                     projectDataModel
                                                                     .Contributors
                                                                     .Select(
                                                                         x =>
                                                                         x.User.UserName)
                                                                     .ToList<string>(),
                    Description =
                                                                     projectDataModel
                                                                     .Description,
                    EstimatedDate =
                                                                     projectDataModel
                                                                     .EstimatedDate,
                    GoalMoney =
                                                                     projectDataModel
                                                                     .GoalMoney,
                    InnovatorId = "2",
                    //Innovator = projectDataModel.Innovator.UserName,
                    IsClosed =
                                                                     projectDataModel
                                                                     .IsClosed,
                };

                return this.Ok(result);
            }

            return this.BadRequest("Project was not found!");
        }

        [Route("Add")]
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

            // TODO: Remove this (just for testing)
            this.publisher.Emit("new-project-added", string.Format("New project was created!"));

            return this.Ok();
        }

        [Route("GetAll")]
        [HttpGet]
        [ValidateModelState]
        [EncryptResultIds]
        //  [Route("projects/getAll")]
        public IHttpActionResult GetAll(int page, int size)
        {
            ICollection<ProjectListItemResponseModel> projectsList =
                this.projects.GetAll(page, size)
                    .Where(x => (!x.IsRemoved))
                    .Select(
                        y =>
                        new ProjectListItemResponseModel
                        {
                            Id = y.Id.ToString(),
                            Name = y.Name,
                            GoalMoney = y.GoalMoney,
                            EstimatedDate = y.EstimatedDate
                        })
                    .ToList<ProjectListItemResponseModel>();

            return this.Ok(projectsList);
        }


        [Route("ProjectAddMoney")]
        //[Authorize]
        [DecryptInputId]
        [HttpPost]
        [ValidateModelState]
        [CheckModelForNull]
        public IHttpActionResult AddMoney(AddProjectMoneyRequestModel moneyRequestModel)
        {
            string userId = this.User.Identity.GetUserId();

            var result = this.projects.AddMoney(int.Parse(moneyRequestModel.Id), moneyRequestModel.MoneyAmount, userId);
            if (result == 1)
            {
                return this.Ok("Your money support was verified!");
            }

            if (result == -1)
            {
                return this.BadRequest("You are not allowed for this opperation!");
            }

            return this.BadRequest("You have no enough money for this donation to be processed! Please chose available amount!");
        }
    }
}