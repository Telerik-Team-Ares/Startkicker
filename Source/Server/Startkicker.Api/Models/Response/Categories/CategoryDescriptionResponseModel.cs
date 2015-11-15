namespace Startkicker.Api.Models.Response.Categories
{
    using System;
    using System.Collections.Generic;

    using Startkicker.Data.Models;

    public class CategoryDescriptionResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}
