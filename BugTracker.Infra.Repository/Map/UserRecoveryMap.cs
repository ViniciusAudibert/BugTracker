using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infra.Repository.Map
{
    public class UserRecoveryMap : EntityTypeConfiguration<ForgotPassword>
    {
        public UserRecoveryMap ()
	    {
            ToTable("UserForgotPassword");
            HasKey(_ => _.IDUserForgotPassword);
            HasRequired(a => a.RequestUser)
                .WithMany(u => u.Forgots)
                .HasForeignKey(a => a.IDUser)
                .WillCascadeOnDelete(false);
        }        
    }
}
