using ControleDeErrosCodenation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Data.Map
{
    public class LevelMap : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.ToTable("Level");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("nvarchar(200)").IsRequired();

            builder.HasMany<Log>(x => x.Logs).WithOne(x => x.Level).HasForeignKey(x => x.IdLevel);

        }
    }
}
