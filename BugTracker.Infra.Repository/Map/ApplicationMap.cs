using BugTracker.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace BugTracker.Infra.Repository.Map
{
    public class ApplicationMap : EntityTypeConfiguration<Application>
    {
        public ApplicationMap()
        {
            ToTable("Application");
            HasKey(x => x.IDApplication);
            HasRequired(a => a.User)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.IDUser);
        }
    }
}
