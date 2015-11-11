namespace Startkicker.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        private ICollection<Project> projects;

        public Category()
        {
            this.projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Project> Projects
        {
            get
            {
                return this.projects;
            }

            set
            {
                this.projects = value;
            }
        }
    }
}