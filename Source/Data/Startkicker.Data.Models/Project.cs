namespace Startkicker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class Project
    {
        private ICollection<User> contributors;

        private ICollection<Image> images;

        public Project()
        {
            this.contributors = new HashSet<User>();
            this.images = new HashSet<Image>();
        }

        public int Id { get; set; }

        [Required]
        [Index]
        [MaxLength(200)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        public DateTime EstimatedDate { get; set; }

        public bool IsClosed { get; set; }

        public bool IsRemoved { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Column(TypeName = "Money")]
        [Range(0, double.MaxValue)]
        public double CollectedMoney { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        [Range(0, double.MaxValue)]
        public double GoalMoney { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("Innovator")]
        public int InnovatorId { get; set; }

        public virtual User Innovator { get; set; }

        public virtual ICollection<Image> Images
        {
            get
            {
                return this.images;
            }

            set
            {
                this.images = value;
            }
        }

        public virtual ICollection<User> Contributors
        {
            get
            {
                return this.contributors;
            }

            set
            {
                this.contributors = value;
            }
        }

        public virtual Category Category { get; set; }
    }
}