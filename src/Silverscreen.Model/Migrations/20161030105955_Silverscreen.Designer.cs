using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Silverscreen.Model;

namespace Silverscreen.Model.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20161030105955_Silverscreen")]
    partial class Silverscreen
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-preview1-22509")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Silverscreen.Model.Directory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DirectoryPath");

                    b.HasKey("Id");

                    b.ToTable("Directories");
                });

            modelBuilder.Entity("Silverscreen.Model.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImdbId");

                    b.Property<string>("Plot");

                    b.Property<string>("Poster");

                    b.Property<int>("Quality");

                    b.Property<string>("Rating");

                    b.Property<string>("Title");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });
        }
    }
}
