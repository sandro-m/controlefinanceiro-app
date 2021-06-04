using ControleFinanceiro.BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Mapeamentos
{
    public class FuncaoMap : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd(); // auto identity
            builder.Property(f => f.Descricao).IsRequired().HasMaxLength(50);

            builder.HasData(
                new Funcao { Id = Guid.NewGuid().ToString(), Name = "Administrador", NormalizedName = "ADMINISTRADOR", Descricao = "Administrador do Sistema" },
                new Funcao { Id = Guid.NewGuid().ToString(), Name = "Usuario", NormalizedName = "USUARIO", Descricao = "Usuário do Sistema" }
                );

            builder.ToTable("Funcoes");
        }
    }
}
