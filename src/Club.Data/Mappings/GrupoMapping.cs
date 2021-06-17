using Club.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Club.Data.Mappings
{
    public class GrupoMapping : IEntityTypeConfiguration<Grupo>
    {
        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.Grupo)
                .HasForeignKey(x => x.GrupoId);

            builder.HasMany(x => x.Integrantes)
                .WithOne(x => x.Grupo)
                .HasForeignKey(x => x.GrupoId);

            builder.ToTable("Grupos");
        }
    }
}
