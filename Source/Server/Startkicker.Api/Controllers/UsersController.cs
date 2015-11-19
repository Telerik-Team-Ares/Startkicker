namespace Startkicker.Api.Controllers
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Startkicker.Api.Models.Response.Projects;
    using Startkicker.Api.Models.Response.Users;
    using Startkicker.Services.Data.Contracts;

    public class UsersController : ApiController
    {
        private readonly IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        [HttpGet]
        //[Authorize]
        public IHttpActionResult Profile(string userName)
        {
            var user = this.users.GetByUserName(userName);

            UserDetailsResponseModel userDetails = new UserDetailsResponseModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MoneyAmount = user.MoneyAmount
            };

            var projects = user.InnovationProjects.AsQueryable().Select(ProjectListItemResponseModel.FromModel).ToList();
            
            userDetails.Projects = projects;

            return this.Ok(userDetails);
        }
    }
}