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
        private ICollection<ContributorsForProjects> contributors;

        private ICollection<Image> images;

        private ICollection<Donation> donations;

        public Project()
        {
            this.contributors = new HashSet<ContributorsForProjects>();
            this.images = new HashSet<Image>();
            this.donations = new HashSet<Donation>();
        }

        public int Id { get; set; }

        [Index]
        [MaxLength(200)]
        [MinLength(5)]
        [Index(IsUnique = true)]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime EstimatedDate { get; set; }

        public bool IsClosed { get; set; }

        public bool IsRemoved { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(20)]
        public string Description { get; set; }

        [Column(TypeName = "Money")]
        [Range(0, int.MaxValue)]
        public int CollectedMoney { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        [Range(10, int.MaxValue)]
        public int GoalMoney { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("Innovator")]
        public string InnovatorId { get; set; }

        public virtual User Innovator { get; set; }

        public virtual Category Category { get; set; }

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

        public virtual ICollection<ContributorsForProjects> Contributors
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

        public virtual ICollection<Donation> Donations
        {
            get
            {
                return this.donations;
            }

            set
            {
                this.donations = value;
            }
        }
    }
}