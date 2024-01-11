﻿using Common.Sockets.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Plugins.DataAccess;

public static class DBExtensions {
    public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration, bool isDev = false) {
        services.AddDbContext<DB>(builder => {
            if (isDev)
                builder.Dev();
            else
                builder.Prod();
        });

        return services;
    }

    public static DbContextOptionsBuilder Dev(this DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .EnableDetailedErrors()
        .UseLoggerFactory(LoggerFactory.Create(logBuilder => logBuilder.AddDebug().AddConsole().SetMinimumLevel(LogLevel.Debug)))
        .EnableSensitiveDataLogging()
        .UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=DevDB;Integrated Security=True;Trust Server Certificate=True", sqliteBuilder => sqliteBuilder.CommandTimeout(60));

    public static DbContextOptionsBuilder Prod(this DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .EnableDetailedErrors()
        .UseLoggerFactory(LoggerFactory.Create(logBuilder => logBuilder.AddDebug().AddConsole().SetMinimumLevel(LogLevel.Debug)))
        .EnableSensitiveDataLogging()
        .UseSqlServer(@"Server=.\SQLEXPRESS;Database=ProdDB;Trusted_Connection=True;", sqliteBuilder => sqliteBuilder.CommandTimeout(60));

    public static DB UseDeveloperDataBase(this WebApplication app, bool delete = false) {
        //app.UseMigrationsEndPoint();

        var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DB>();

        db.Schema(delete);

        //db.Data(new Tag(1, "Tag1"),
        //        new Tag(2, "Tag2"),
        //        new Tag(3, "Tag3"));

        //db.Data(new Post(1, "Title1", "Content1", DateTime.Parse("2023-12-01")),
        //        new Post(2, "Title2", "Content2", DateTime.Parse("2023-12-02")),
        //        new Post(3, "Title3", "Content3", DateTime.Parse("2023-12-03")));

        return db;
    }

    public static DB Schema(this DB db, bool delete = false) {
        //if (delete)
        //db.Database.EnsureDeleted();
        //db.Database.EnsureCreated();
        //db.Database.Migrate();

        return db;
    }
    public static void Data<T>(this DB db, params T[] list) where T : class => db.Data(list);
    public static DB Data<T>(this DB db, IEnumerable<T> list) where T : class {
        var set = db.Set<T>();
        if (!set.Any()) {
            set.AddRange(list);
            db.SaveChanges();
        }
        return db;
    }
}