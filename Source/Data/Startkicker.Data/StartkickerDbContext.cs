namespace Startkicker.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Startkicker.Data.Models;

    public class StartkickerDbContext : IdentityDbContext<User>, IStartkickerDbContext
    {
        public StartkickerDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Project> Projects { get; set; }

        public virtual IDbSet<ContributorsForProjects> ContributorsForProjects { get; set; }

        public virtual IDbSet<Donation> Donations { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public static StartkickerDbContext Create()
        {
            return new StartkickerDbContext();
        }
    }
}
