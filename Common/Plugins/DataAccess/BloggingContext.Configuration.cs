﻿using Core.PluginAdapters.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Shared.Technology.DataAccess;

public partial class BloggingContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "Blogging.db");
        options.UseSqlite($"Data Source={dbPath}");

        //options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={"Blogging"};Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().ToTable("Post");
        modelBuilder.Entity<Tag>().ToTable("Tag");
    }

    public void EnsureInitialized()
    {
        var seeded = Posts.Any();
        if (seeded) return;

        InitTags();
        InitPosts();

        void InitTags()
        {
            var tags = new Tag[]
            {
            new Tag{Id=1,Name="Tag1" },
            new Tag{Id=2,Name="Tag2" },
            new Tag{Id=3,Name="Tag3" },
            };
            Tags.AddRange(tags);
            SaveChanges();
        }

        void InitPosts()
        {
            var posts = new Post[]
            {
            new Post{ Id=1, Title="Title1",Content="Content1",CreatedAt=DateTime.Parse("2023-12-01")},
            new Post{ Id=2, Title="Title2",Content="Content2",CreatedAt=DateTime.Parse("2023-12-02")},
            new Post{ Id=3, Title="Title3",Content="Content3",CreatedAt=DateTime.Parse("2023-12-03")}
            };
            Posts.AddRange(posts);
            SaveChanges();
        }
    }
}