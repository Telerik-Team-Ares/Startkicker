namespace Startkicker.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Startkicker.Data.Models;

    public class StartkickerDbContext : IdentityDbContext<User>, IStartkickerDbContext
    {
        public StartkickerDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static StartkickerDbContext Create()
        {
            return new StartkickerDbContext();
        }
    }
}
