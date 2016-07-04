using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infra.Repository.Map
{
    class ActivationMap : EntityTypeConfiguration<UserActivation>
    {
        public ActivationMap()
	    {
	        ToTable("UserActivation");
                HasKey(a => a.IDUserActivation);

            HasRequired(a => a.User)
                .WithMany(u => u.Activations)
                .HasForeignKey(a => a.IDUser);
        }
    }
}
