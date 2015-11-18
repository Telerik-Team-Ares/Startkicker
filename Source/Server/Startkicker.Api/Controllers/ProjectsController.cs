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
    using System.Threading.Tasks;

    [RoutePrefix("api/Projects")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectsService projects;
        private readonly IImagesService images;
        private readonly IPublisher publisher;

        public ProjectsController(IProjectsService projects, IImagesService images, IPublisher publisher)
        {
            this.projects = projects;
            this.images = images;
            this.publisher = publisher;
        }

        [HttpGet]
        //[Route("GetById")]
        //[Authorize]
        //[DecryptInputId]
        //[EncryptResultIds]
        //[Route("projects")]
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
                    //InnovatorId = "2",
                    Innovator = projectDataModel.Innovator.UserName,
                    IsClosed =
                                                                     projectDataModel
                                                                     .IsClosed,
                };

                return this.Ok(result);
            }

            return this.BadRequest("Project was not found!");
        }

        // [Route("Add")]
        [HttpPost]
        [ValidateModelState]
        [CheckModelForNull]
        //[DecryptInputId]
        [Authorize]
        public async Task<IHttpActionResult> Add(NewProjectRequestModel projectModel)
        {
            var projectToAdd = new Project
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
            };


            var imageUrl = await images.UploadAsync(projectModel.Image.ByteArrayContent, projectModel.Image.FileExtension);

            var projectImage = new Image
            {
                ImageUrl = imageUrl
            };

            projectToAdd.Images = new List<Image>() { projectImage };

            this.projects.Add(projectToAdd);

            // TODO: Remove this (just for testing)
            this.publisher.Emit("new-project-added", string.Format("New project was created!"));

            return this.Ok();
        }

        [HttpGet]
        [ValidateModelState]
        // [EncryptResultIds]
        //  [Route("projects/getAll")]
        public IHttpActionResult GetAll(int page = 1, int size = 10)
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
                            EstimatedDate = y.EstimatedDate,
                            CollectedMoney = y.CollectedMoney
                        })
                    .ToList<ProjectListItemResponseModel>();

            return this.Ok(projectsList);
        }


        //[Route("ProjectAddMoney")]
        //[Authorize]
        //[DecryptInputId]
        [HttpPut]
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

        [HttpDelete]
        // [DecryptInputId]
        public IHttpActionResult Remove(string id)
        {
            int idToInt = int.Parse(id);

            this.projects.RemoveById(idToInt);

            return this.Ok("Project has been removed!");
        }
    }
}