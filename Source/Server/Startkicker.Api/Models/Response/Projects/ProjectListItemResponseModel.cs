﻿namespace Startkicker.Api.Models.Response.Projects
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Startkicker.Data.Models;
    
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
                    Category = pr.Category.Name,
                    Innovator = pr.Innovator.UserName,
                    GoalMoney = pr.GoalMoney,
                    EstimatedDate = pr.EstimatedDate,
                    CollectedMoney = pr.CollectedMoney,
                    ImageUrl = pr.Images.FirstOrDefault().ImageUrl
                };
            }
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Innovator { get; set; }

        public DateTime EstimatedDate { get; set; }

        public int GoalMoney { get; set; }

        public int CollectedMoney { get; set; }

        public string ImageUrl { get; set; }
    }
}