using Club.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Biografia)
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.NomeUsuario)
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(x => x.Site)
                .HasColumnType("varchar(100)");

            builder.HasMany(x => x.Grupos)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasMany(x => x.PostsFeed)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasMany(x => x.Participados)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId);

            builder.ToTable("Usuarios");

        }
    }
}
