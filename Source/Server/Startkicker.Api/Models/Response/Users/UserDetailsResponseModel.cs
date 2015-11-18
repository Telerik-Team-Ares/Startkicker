namespace Startkicker.Api.Models.Response.Users
{
    using System.Collections.Generic;

    using Startkicker.Api.Models.Response.Projects;

    public class UserDetailsResponseModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int MoneyAmount { get; set; }

        public ICollection<ProjectListItemResponseModel> Projects { get; set; }
    }
}