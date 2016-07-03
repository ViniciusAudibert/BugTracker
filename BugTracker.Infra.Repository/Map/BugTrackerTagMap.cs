using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infra.Repository.Map
{
    public class BugTrackerTagMap : EntityTypeConfiguration<BugTrackerTag>
    {
        public BugTrackerTagMap()
        {
            ToTable("BugTrackerTag");
            HasKey(_ => _.IDBugTrackerTag);
            HasRequired(t => t.BugTracker)
                .WithMany(b => b.Tags)
                .HasForeignKey(t => t.IDBugTracker)
                .WillCascadeOnDelete(false);
        }
    }
}
