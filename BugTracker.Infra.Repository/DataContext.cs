using BugTracker.Domain.Entity;
using BugTracker.Infra.Repository.Map;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Dominio = BugTracker.Domain.Entity;

namespace BugTracker.Infra.Repository
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Conn"){ }

        public DbSet<User> User { get; set; }
        public DbSet<ForgotPassword> UserRecovery { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Dominio.BugTracker> BugTrucker { get; set; }
        public DbSet<BugTrackerTag> BugTrackerTag { get; set; }
        public DbSet<UserActivation> Activation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
             .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Properties()
                .Where(p => p.Name == "ID" + p.ReflectedType.Name)
                .Configure(p => p.IsKey());

            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserRecoveryMap());
            modelBuilder.Configurations.Add(new ActivationMap());
            modelBuilder.Configurations.Add(new ApplicationMap());
            modelBuilder.Configurations.Add(new BugTrackerMap());
            modelBuilder.Configurations.Add(new BugTrackerTagMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
