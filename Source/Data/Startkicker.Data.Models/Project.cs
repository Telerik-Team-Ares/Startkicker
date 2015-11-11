namespace Startkicker.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        private ICollection<User> users;

        private ICollection<Image> images;

        public Project()
        {
            this.users = new HashSet<User>();
            this.images = new HashSet<Image>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

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

        public virtual ICollection<User> Users
        {
            get
            {
                return this.users;
            }

            set
            {
                this.users = value;
            }
        }

        public virtual Category Category { get; set; }
    }
}