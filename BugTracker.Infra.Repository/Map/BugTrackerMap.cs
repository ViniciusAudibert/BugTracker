using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio = BugTracker.Domain.Entity;

namespace BugTracker.Infra.Repository.Map
{
    public class BugTrackerMap : EntityTypeConfiguration<Dominio.BugTracker>
    {
        public BugTrackerMap()
        {
            ToTable("BugTracker");
            HasKey(_ => _.IDBugTracker);

            Property(_ => _.Browser.Name).HasColumnName("BrowserName");
            Property(_ => _.Browser.Version).HasColumnName("BrowserVersion");
            Property(_ => _.OperationalSystem.Name).HasColumnName("PlatformName");
        }
    }
}
