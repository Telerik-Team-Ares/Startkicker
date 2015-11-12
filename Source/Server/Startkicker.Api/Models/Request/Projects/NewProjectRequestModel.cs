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
        [MaxLength(1000)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public double CollectedMoney { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double GoalMoney { get; set; }

        public int CategoryId { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}