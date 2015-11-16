namespace Startkicker.Services.Data.Contracts
{
    using Startkicker.Data.Models;

    public interface IUsersService
    {
        User GetById(string id);
    }
}
