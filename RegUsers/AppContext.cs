using System.Data.Entity; // технология, позволяющая работать с СУБД

namespace RegUsers
{
    // связь с БД
    class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppContext() : base("DefaultConnection") { }
    }
}