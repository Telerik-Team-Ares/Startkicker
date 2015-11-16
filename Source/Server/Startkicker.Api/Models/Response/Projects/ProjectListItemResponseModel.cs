namespace Startkicker.Api.Models.Response.Projects
{
    using System;

    public class ProjectListItemResponseModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime EstimatedDate { get; set; }

        public int GoalMoney { get; set; }
    }
}