﻿namespace Startkicker.Api.Models.Response.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Startkicker.Api.Models.Response.Donations;
    using Startkicker.Data.Models;

    public class ProjectDescriptionResponseModel
    {
        public static Expression<Func<Project, ProjectDescriptionResponseModel>> FromModel
        {
            get
            {
                return pr => new ProjectDescriptionResponseModel
                {
                    CategoryName = pr.Category.Name,
                    Name = pr.Name,
                    CollectedMoney = pr.CollectedMoney,
                    Description = pr.Description,
                    EstimatedDate = pr.EstimatedDate,
                    GoalMoney = pr.GoalMoney,
                    Innovator = pr.Innovator.UserName,
                    IsClosed = pr.IsClosed,
                    Images = pr.Images.Select(im => im.ImageUrl).ToList(),
                    Id = pr.Id,
                    Donations = pr.Donations.AsQueryable().Select(DonationResponseModel.FromModel).ToList()
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime EstimatedDate { get; set; }

        public bool IsClosed { get; set; }

        public string Description { get; set; }

        public int CollectedMoney { get; set; }

        public int GoalMoney { get; set; }

        public string Innovator { get; set; }

        public ICollection<string> Images { get; set; }

        public string CategoryName { get; set; }

        public ICollection<DonationResponseModel> Donations { get; set; }
    }
}