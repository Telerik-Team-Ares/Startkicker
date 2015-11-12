namespace Startkicker.Api.Controllers
{
    using System.Web.Http;
    using System.Web.UI.WebControls;

    using Startkicker.Api.Models.Request.Projects;
    using Startkicker.Services.Data.Contracts;

    public class ProjectsController : ApiController
    {
        private readonly IProjectsService projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        public IHttpActionResult GetById(string idHash)
        {
            return this.Ok();
        }

        public IHttpActionResult AddProject(NewProjectRequestModel projectModel)
        {
            return this.Ok();
        }
    }
}