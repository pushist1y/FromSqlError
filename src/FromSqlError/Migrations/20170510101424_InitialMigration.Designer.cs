using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FromSqlError;

namespace FromSqlError.Migrations
{
    [DbContext(typeof(CommonContext))]
    [Migration("20170510101424_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("FromSqlError.ModelFilter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("NumData");

                    b.HasKey("Id");

                    b.ToTable("ModelFilter");
                });

            modelBuilder.Entity("FromSqlError.ModelMain", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("NumData");

                    b.HasKey("Id");

                    b.ToTable("MainModels");
                });
        }
    }
}
