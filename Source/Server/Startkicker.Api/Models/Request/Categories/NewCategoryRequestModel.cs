namespace Startkicker.Api.Models.Request.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Startkicker.Data.Models;

    public class NewCategoryRequestModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(5)]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}
