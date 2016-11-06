using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-preview1-22509")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Silverscreen.Core.Model.LibraryDirectory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DirectoryPath");

                    b.HasKey("Id");

                    b.HasIndex("DirectoryPath")
                        .IsUnique();

                    b.ToTable("Directories");
                });

            modelBuilder.Entity("Silverscreen.Core.Model.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DesiredQuality");

                    b.Property<string>("ImdbId");

                    b.Property<string>("Plot");

                    b.Property<string>("Poster");

                    b.Property<int>("Quality");

                    b.Property<string>("Rating");

                    b.Property<string>("Title");

                    b.Property<int>("Year");

                    b.Property<bool>("inLibrary");

                    b.HasKey("Id");

                    b.HasIndex("ImdbId")
                        .IsUnique();

                    b.ToTable("Movies");
                });
        }
    }
}
