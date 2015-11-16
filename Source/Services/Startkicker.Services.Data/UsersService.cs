namespace Startkicker.Services.Data
{
    using Startkicker.Data.Models;
    using Startkicker.Data.Repositories;
    using Startkicker.Services.Data.Contracts;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> usersRepo;

        public UsersService(IRepository<User> usersRepo)
        {
            this.usersRepo = usersRepo;
        }   

        public User GetById(string id)
        {
            return this.usersRepo.GetById(id);
        }
    }
}
