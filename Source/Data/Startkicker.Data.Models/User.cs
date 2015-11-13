namespace Startkicker.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<ContributorsForProjects> contributionProjects;

        private ICollection<Project> innovationProjects;

        public User()
        {
            this.contributionProjects = new HashSet<ContributorsForProjects>();
            this.innovationProjects = new HashSet<Project>();
        }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string LastName { get; set; }

        public virtual ICollection<Project> InnovationProjects
        {
            get
            {
                return this.innovationProjects;
            }

            set
            {
                this.innovationProjects = value;
            }
        }

        public virtual ICollection<ContributorsForProjects> ContributionProjects
        {
            get
            {
                return this.contributionProjects;
            }

            set
            {
                this.contributionProjects = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
