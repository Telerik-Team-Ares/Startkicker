namespace Startkicker.Api.Models.Response.Projects
{
    using Data.Models;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class ProjectListItemResponseModel
    {
        public static Expression<Func<Project, ProjectListItemResponseModel>> FromModel
        {
            get
            {
                return pr => new ProjectListItemResponseModel
                {
                    Id = pr.Id.ToString(),
                    Name = pr.Name,
                    GoalMoney = pr.GoalMoney,
                    EstimatedDate = pr.EstimatedDate,
                    CollectedMoney = pr.CollectedMoney,
                    ImageUrl = pr.Images.FirstOrDefault().ImageUrl
                };
            }
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime EstimatedDate { get; set; }

        public int GoalMoney { get; set; }

        public int CollectedMoney { get; set; }

        public string ImageUrl { get; set; }
    }
}