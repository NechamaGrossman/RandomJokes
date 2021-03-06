﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReactJokesHw.Data;

namespace ReactJokesHw.Data.Migrations
{
    [DbContext(typeof(JokesContext))]
    [Migration("20200531222946_Tables")]
    partial class Tables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReactJokesHw.Data.Jokes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Punchline");

                    b.Property<string>("Setup");

                    b.HasKey("Id");

                    b.ToTable("Jokes");
                });

            modelBuilder.Entity("ReactJokesHw.Data.UserLikedJokes", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("JokeId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Id");

                    b.Property<bool>("Liked");

                    b.HasKey("UserId", "JokeId");

                    b.HasIndex("JokeId");

                    b.ToTable("UserLikedJokes");
                });

            modelBuilder.Entity("ReactJokesHw.Data.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReactJokesHw.Data.UserLikedJokes", b =>
                {
                    b.HasOne("ReactJokesHw.Data.Jokes", "Joke")
                        .WithMany("UserjokeLikes")
                        .HasForeignKey("JokeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ReactJokesHw.Data.Users", "User")
                        .WithMany("UserLikedJokes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
