using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using ToroBank.Domain.Common;

namespace ToroBank.Infrastructure.Persistence.Context.Configurations
{
    public class BaseAuditableEntityConfiguration<T, TKey> : IEntityTypeConfiguration<T>
    where TKey : IEquatable<TKey>
    where T : BaseAuditableEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Identity
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            // Audit
            builder.Property(t => t.Created)
                .IsRequired();

            builder.Property(t => t.CreatedBy)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.LastModifiedBy)
                .HasMaxLength(255);

        }
    }
}
