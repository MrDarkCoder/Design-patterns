using MessageBroker.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageBroker.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<Message> Messages => Set<Message>();
    }
}
