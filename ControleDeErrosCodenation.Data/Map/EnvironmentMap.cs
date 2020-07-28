using ControleDeErrosCodenation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Environment = ControleDeErrosCodenation.Domain.Models.Environment;

namespace ControleDeErrosCodenation.Data.Map
{
    public class EnvironmentMap : IEntityTypeConfiguration<Environment>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Environment> builder)
        {
            builder.ToTable("Environment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("nvarchar(200)").IsRequired();

            builder.HasMany<Log>(x => x.Logs).WithOne(x => x.Environment).HasForeignKey(x => x.IdEnvironment);
        }
    }
}
