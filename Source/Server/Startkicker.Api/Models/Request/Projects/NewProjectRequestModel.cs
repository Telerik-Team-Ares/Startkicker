namespace Startkicker.Api.Models.Request.Projects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using Startkicker.Data.Models;

    public class NewProjectRequestModel
    {
        [Required]
        [MaxLength(200)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(20)]
        public string Description { get; set; }

        [Required]
        [Range(2, 366)]
        public int EstimatedDays { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GoalMoney { get; set; }

        public int CategoryId { get; set; }

        //public ICollection<Image> Images { get; set; }

        //public ICollection<User> Contributors{get; set;}

    }
}