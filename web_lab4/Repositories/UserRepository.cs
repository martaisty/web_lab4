using System.Linq;
using Microsoft.EntityFrameworkCore;
using web_lab4.Abstractions;
using web_lab4.Models;

namespace web_lab4.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        protected override IQueryable<User> ComplexEntities =>
            Context.Users
                .AsTracking();
    }
}