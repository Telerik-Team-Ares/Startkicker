namespace Startkicker.Api.Controllers
{   
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Startkicker.Api.Common.Contracts;
    using Startkicker.Api.Infrastructure.ActionFilters;
    using Startkicker.Api.Models.Request.Projects;
    using Startkicker.Api.Models.Response.Projects;
    using Startkicker.Data.Models;
    using Startkicker.Services.Data.Contracts;

    [RoutePrefix("api/Projects")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectsService projects;
        private readonly IUsersService users;
        private readonly IImagesService images;
        private readonly IPublisher publisher;

        public ProjectsController(IProjectsService projects, IUsersService users, IImagesService images, IPublisher publisher)
        {
            this.projects = projects;
            this.users = users;
            this.images = images;
            this.publisher = publisher;
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetById(int id)
        {
            var result = this.projects
                .GetById(id)
                .Select(ProjectDescriptionResponseModel.FromModel)
                .FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("Project was not found!");
            }

            return this.Ok(result);
        }

        [HttpPost]
        [ValidateModelState]
        [CheckModelForNull]
        [Authorize]
        public async Task<IHttpActionResult> Add(NewProjectRequestModel project)
        {
            string projectUserId = this.User.Identity.GetUserId();
            var projectImages = new List<Image>();

            foreach (var image in project.Images)
            {
                var imageUrl = await images.UploadAsync(image.ByteArrayContent, image.FileExtension);
                projectImages.Add(new Image { ImageUrl = imageUrl });
            }
            
            int projectId = this.projects
                .Add(project.Name, project.Description, project.GoalMoney, project.EstimatedDays, project.CategoryId, projectUserId, projectImages);

            return this.Ok(projectId);
        }

        [HttpGet]
        [ValidateModelState]
        public IHttpActionResult GetAll(int? page)
        {
            var result = this.projects
                .GetAll(1, 10)
                .Select(ProjectListItemResponseModel.FromModel)
                .ToList();

            return this.Ok(result);
        }

        [HttpPut]
        [ValidateModelState]
        [CheckModelForNull]
        public IHttpActionResult AddMoney(AddProjectMoneyRequestModel moneyRequestModel)
        {
            string userId = this.User.Identity.GetUserId();

            var result = this.projects.AddMoney(
                int.Parse(moneyRequestModel.Id), 
                moneyRequestModel.MoneyAmount, 
                userId);

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
        public IHttpActionResult Remove(string id)
        {
            int idToInt = int.Parse(id);

            this.projects.RemoveById(idToInt);

            return this.Ok("Project has been removed!");
        }
    }
}