using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Startkicker.Api.Controllers
{
    using System.Web.Http;
    using System.Web.UI.WebControls;

    using Startkicker.Api.Models.Request.Projects;

    public class ProjectsController : ApiController
    {
        // GET: Projects
        public IHttpActionResult Get()
        {
            return this.Ok();
        }

        public IHttpActionResult AddProject(AddNewProjectModel projectModel)
        {
            return this.Ok();
        }
    }
}