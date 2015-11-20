namespace Startkicker.Api.Models.Response.Categories
{
    using System;
    using System.Linq.Expressions;

    using Startkicker.Data.Models;

    public class CategoryDescriptionResponseModel
    {
        public static Expression<Func<Category, CategoryDescriptionResponseModel>> FromModel
        {
            get
            {
                return c => new CategoryDescriptionResponseModel
                {
                    Name = c.Name,
                    Id = c.Id
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}