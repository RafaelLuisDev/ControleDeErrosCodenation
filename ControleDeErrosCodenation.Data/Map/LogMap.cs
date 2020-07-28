using ControleDeErrosCodenation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Data.Map
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Log");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.Details).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.Origin).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.Archived).HasColumnType("bit").IsRequired();
            builder.Property(x => x.Date).HasColumnType("smalldatetime").HasDefaultValueSql("getdate()").IsRequired();

        }

    }
}
