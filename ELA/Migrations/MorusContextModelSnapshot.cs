﻿// <auto-generated />
using System;
using ELA.Models.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ELA.Migrations
{
    [DbContext(typeof(MorusContext))]
    partial class MorusContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ArtigoAssunto", b =>
                {
                    b.Property<int>("ArtigoId")
                        .HasColumnType("int");

                    b.Property<int>("AssuntosId")
                        .HasColumnType("int");

                    b.Property<int?>("AssuntoId")
                        .HasColumnType("int");

                    b.HasKey("ArtigoId", "AssuntosId");

                    b.HasIndex("AssuntoId");

                    b.HasIndex("AssuntosId");

                    b.ToTable("ArtigoAssunto");
                });

            modelBuilder.Entity("AssuntoFiqueAtento", b =>
                {
                    b.Property<int>("AssuntosId")
                        .HasColumnType("int");

                    b.Property<int>("FiqueAtentoId")
                        .HasColumnType("int");

                    b.HasKey("AssuntosId", "FiqueAtentoId");

                    b.HasIndex("FiqueAtentoId");

                    b.ToTable("AssuntoFiqueAtento");
                });

            modelBuilder.Entity("AssuntoPergunta", b =>
                {
                    b.Property<int>("AssuntosId")
                        .HasColumnType("int");

                    b.Property<int>("PerguntaId")
                        .HasColumnType("int");

                    b.Property<int?>("AssuntoId")
                        .HasColumnType("int");

                    b.HasKey("AssuntosId", "PerguntaId");

                    b.HasIndex("AssuntoId");

                    b.HasIndex("PerguntaId");

                    b.ToTable("AssuntoPergunta");
                });

            modelBuilder.Entity("ELA.Models.Artigo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataPostagem")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Resumo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SubTitulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Artigos");
                });

            modelBuilder.Entity("ELA.Models.Assunto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Assuntos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Infantil"
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "Meninas"
                        },
                        new
                        {
                            Id = 3,
                            Descricao = "Meninos"
                        });
                });

            modelBuilder.Entity("ELA.Models.FiqueAtento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataPostagem")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Resumo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("FiqueAtentos");
                });

            modelBuilder.Entity("ELA.Models.Pergunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataPostagem")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Resposta")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Resumo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Perguntas");
                });

            modelBuilder.Entity("ELA.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TipoUsuarioEnum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CPF = "123.123.123-12",
                            DataNascimento = new DateTime(1987, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "bella.swan@email.com",
                            Nome = "Isabella Swan",
                            Senha = "edwardJacob",
                            TipoUsuarioEnum = 3
                        },
                        new
                        {
                            Id = 2,
                            CPF = "122.123.123-12",
                            DataNascimento = new DateTime(1987, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "katnip@email.com",
                            Nome = "Katniss Everdeen",
                            Senha = "girlOnFire",
                            TipoUsuarioEnum = 0
                        });
                });

            modelBuilder.Entity("ArtigoAssunto", b =>
                {
                    b.HasOne("ELA.Models.Artigo", null)
                        .WithMany()
                        .HasForeignKey("ArtigoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELA.Models.Assunto", null)
                        .WithMany()
                        .HasForeignKey("AssuntoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ELA.Models.Assunto", null)
                        .WithMany()
                        .HasForeignKey("AssuntosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssuntoFiqueAtento", b =>
                {
                    b.HasOne("ELA.Models.Assunto", null)
                        .WithMany()
                        .HasForeignKey("AssuntosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELA.Models.FiqueAtento", null)
                        .WithMany()
                        .HasForeignKey("FiqueAtentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssuntoPergunta", b =>
                {
                    b.HasOne("ELA.Models.Assunto", null)
                        .WithMany()
                        .HasForeignKey("AssuntoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ELA.Models.Assunto", null)
                        .WithMany()
                        .HasForeignKey("AssuntosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELA.Models.Pergunta", null)
                        .WithMany()
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ELA.Models.Artigo", b =>
                {
                    b.HasOne("ELA.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ELA.Models.FiqueAtento", b =>
                {
                    b.HasOne("ELA.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ELA.Models.Pergunta", b =>
                {
                    b.HasOne("ELA.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
