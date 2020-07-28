using ControleDeErrosCodenation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.Password).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.Role).HasColumnType("nvarchar(200)").IsRequired();
        }
    }
}
