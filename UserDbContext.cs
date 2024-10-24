using Microsoft.EntityFrameworkCore;
namespace szamonkeresapi.Modles

public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> mock_data { get; set; }
    }

}
}
