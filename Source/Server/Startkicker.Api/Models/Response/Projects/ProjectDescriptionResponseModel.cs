namespace Startkicker.Api.Models.Response.Projects
{
    using System;
    using System.Collections.Generic;

    using Startkicker.Data.Models;

    public class ProjectDescriptionResponseModel
    {
        public string Name { get; set; }

        public DateTime EstimatedDate { get; set; }

        public bool IsClosed { get; set; }

        public string Description { get; set; }

        public int CollectedMoney { get; set; }

        public int GoalMoney { get; set; }

        public string InnovatorId { get; set; }

        public string Innovator { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<string> Contributors { get; set; }

        public string CategoryName { get; set; }
    }
}