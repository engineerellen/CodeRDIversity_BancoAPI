using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RepositoryEntity.Models;

namespace RepositoryEntity.Context;

public partial class BancoContext : DbContext
{

    public BancoContext()
    {

    }

    public BancoContext(DbContextOptions<BancoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContaPessoaFisica> ContaPessoaFisicas { get; set; }

    public virtual DbSet<ContaPessoaJuridica> ContaPessoaJuridicas { get; set; }

    public virtual DbSet<Contum> Conta { get; set; }

    public virtual DbSet<TipoContum> TipoConta { get; set; }

    public virtual DbSet<Historico> Historicos { get; set; }

    public virtual DbSet<TipoTransacao> TipoTransacaos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContaPessoaFisica>(entity =>
        {
            entity.HasKey(e => e.IdContaPf)
                .HasName("PK_ID_CONTA_CORRENTE_PF")
                .IsClustered(false);

            entity.ToTable("CONTA_PESSOA_FISICA");

            entity.Property(e => e.IdContaPf).HasColumnName("ID_CONTA_PF");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("CPF");
            entity.Property(e => e.Endereco)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdConta).HasColumnName("ID_CONTA");
            entity.Property(e => e.NomeCliente)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Profissao)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RendaFamiliar).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdContaNavigation).WithMany(p => p.ContaPessoaFisicas)
                .HasForeignKey(d => d.IdConta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_CONTA_CORRENTE_PF");
        });

        modelBuilder.Entity<ContaPessoaJuridica>(entity =>
        {
            entity.HasKey(e => e.IdContaPj)
                .HasName("PK_ID_CONTA_CORRENTE_PJ")
                .IsClustered(false);

            entity.ToTable("CONTA_PESSOA_JURIDICA");

            entity.Property(e => e.IdContaPj).HasColumnName("ID_CONTA_PJ");
            entity.Property(e => e.Cnpj)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("CNPJ");
            entity.Property(e => e.FaturamentoMedio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IdConta).HasColumnName("ID_CONTA");
            entity.Property(e => e.NomeFantasia)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("Nome_Fantasia");
            entity.Property(e => e.RazaoSocial)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("Razao_Social");
            entity.Property(e => e.ValorInicial).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdContaNavigation).WithMany(p => p.ContaPessoaJuridicas)
                .HasForeignKey(d => d.IdConta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_CONTA_CORRENTE");
        });

        modelBuilder.Entity<Contum>(entity =>
        {
            entity.HasKey(e => e.IdConta)
                .HasName("PK_ID_CONTA_CORRENTE")
                .IsClustered(false);

            entity.ToTable("CONTA");

            entity.Property(e => e.IdConta).HasColumnName("ID_CONTA");
            entity.Property(e => e.Agencia)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Digito)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.IdTipoConta).HasColumnName("ID_TIPO_CONTA");
            entity.Property(e => e.NomeConta)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NumeroConta)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Pix)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PIX");
            entity.Property(e => e.ValorConta).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdTipoContaNavigation).WithMany(p => p.Conta)
                .HasForeignKey(d => d.IdTipoConta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_TIPO_CONTA_CONTA_CORRENTE_TIPO_CONTA");
        });

        modelBuilder.Entity<TipoContum>(entity =>
        {
            entity.HasKey(e => e.IdTipoConta)
                .HasName("PK_ID_TIPO_CONTA")
                .IsClustered(false);

            entity.ToTable("TIPO_CONTA");

            entity.Property(e => e.IdTipoConta).HasColumnName("ID_TIPO_CONTA");
            entity.Property(e => e.Codigo).HasColumnName("CODIGO");
            entity.Property(e => e.DescricaoTipo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRICAO_TIPO");
        });

        modelBuilder.Entity<Historico>(entity =>
        {
            entity.HasKey(e => e.IdHistorico)
              .HasName("PK_HISTORICO")
                .IsClustered(true);

            entity.ToTable("HISTORICO");

            entity.Property(e => e.IdHistorico).HasColumnName("ID_HISTORICO");
            entity.Property(e => e.DtTransacao)
                .HasColumnType("datetime")
                .HasColumnName("DT_Transacao");
            entity.Property(e => e.IdConta).HasColumnName("ID_CONTA");
            entity.Property(e => e.IdTipoTransacao).HasColumnName("ID_TipoTransacao");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("VALOR");

            entity.HasOne(d => d.IdTipoTransacaoNavigation).WithMany(p => p.Historicos)
               .HasForeignKey(d => d.IdTipoTransacao)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_HISTORICO_TipoTransacao");
        });


        modelBuilder.Entity<TipoTransacao>(entity =>
        {
            entity.HasKey(e => e.IdTipoTransacao);

            entity.ToTable("TipoTransacao");

            entity.Property(e => e.IdTipoTransacao).HasColumnName("ID_TipoTransacao");
            entity.Property(e => e.Codigo).HasColumnName("CODIGO");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
