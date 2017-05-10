using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FromSqlError;

namespace FromSqlError.Migrations
{
    [DbContext(typeof(CommonContext))]
    partial class CommonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
