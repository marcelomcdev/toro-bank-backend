using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToroBank.Domain.Entities;

namespace ToroBank.Infrastructure.Persistence.Context.Configurations
{    

    public class UserConfiguration : BaseAuditableEntityConfiguration<User, int>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(f => f.AccountNumber).IsRequired();
            builder.Property(f => f.CPF).HasMaxLength(20).IsRequired();
            builder.Property(f => f.Balance).IsRequired();
            builder.Property(f => f.Username).IsRequired();
            builder.Property(f => f.Password).IsRequired();
        }
    }


}
