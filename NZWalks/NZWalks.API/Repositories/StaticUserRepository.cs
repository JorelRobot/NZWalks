using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                FirstName = "Joel", LastName = "Rueda", EmailAddress = "jorel.robot@user.com",
                Id = Guid.NewGuid(), Username = "JorelRobot", Password = "genericpass",
                Roles = new List<string>() {"reader", "writer"}
            },
            new User()
            {
                FirstName = "Fernando", LastName = "Sustaita", EmailAddress = "fervnm155@user.com",
                Id = Guid.NewGuid(), Username = "Fervnm155", Password = "genericpass",
                Roles = new List<string>() {"reader"}
            }
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);

            return user;
        }
    }
}
