﻿using Common.Solutions.Data.MainDB.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common.Solutions.Data.MainDB;

public static class Extensions {

    public static IServiceCollection AddMyLibraryService2(this IServiceCollection services, Action<TransientFaultHandlingOptions> optionBuilder = null) => services
       .AddOptionsWithValidateOnStart<TransientFaultHandlingOptions>()
       .Configure<IConfiguration, IHostEnvironment>((options, config, env) => {
           config
           .GetSection(TransientFaultHandlingOptions.SectionName)
           .Bind(options);

           optionBuilder ??= _ => { };
           optionBuilder(options);
       })
       .ValidateDataAnnotations()
       .Validate(options => !options.Enabled || options.AutoRetryDelay > TimeSpan.Zero, "AutoRetryDelay must be set if Enabeled.")
       .Services;


    public static IServiceCollection AddCommonSolutions(this IServiceCollection services) => services
       .AddMainDB();

    public static IServiceCollection AddMainDB(this IServiceCollection services) {
        // var v = configuration.connectionString("MainDB");   
        services.AddDbContext<MainDB>();
        //services.AddDbContext<MainDB>((sp, builder) => {
        //var solutions = sp.GetService<Solutions>();
        //var connection = solutions.Data.MainDB.ConnectionString;
        //builder.UseSqlServer(connection);
        //if (environment == Environments.Development)
        //    optionsBuilder.Dev();
        //else
        //    optionsBuilder.Prod();
        //});
        return services;
    }

    public static DbContextOptionsBuilder Dev(this DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .EnableDetailedErrors()
        //.UseLoggerFactory(LoggerFactory.Create(logBuilder => logBuilder.AddConfiguration(configuration)))
        .EnableSensitiveDataLogging()
        //.UseSqlServer(configuration.GetConnectionString("MainDB"), sqlServerOptionsBuilder => sqlServerOptionsBuilder.CommandTimeout(60));
        .UseSqlServer("MainDB", sql => sql.CommandTimeout(60));

    public static DbContextOptionsBuilder Prod(this DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .EnableDetailedErrors()
        //.UseLoggerFactory(LoggerFactory.Create(logBuilder => logBuilder.AddConfiguration(configuration)))
        .EnableSensitiveDataLogging()
        .UseSqlServer("MainDB", sqliteBuilder => sqliteBuilder.CommandTimeout(60));

    public static MainDB UseDeveloperDataBase(this WebApplication app, bool delete = false) {
        //app.UseMigrationsEndPoint();

        var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MainDB>();

        db.Schema(delete);

        //db.Data(new Tag(1, "Tag1"),
        //        new Tag(2, "Tag2"),
        //        new Tag(3, "Tag3"));

        //db.Data(new Post(1, "Title1", "Content1", DateTime.Parse("2023-12-01")),
        //        new Post(2, "Title2", "Content2", DateTime.Parse("2023-12-02")),
        //        new Post(3, "Title3", "Content3", DateTime.Parse("2023-12-03")));

        return db;
    }

    public static MainDB Schema(this MainDB db, bool delete = false) {
        if (delete)
            db.Database.EnsureDeleted();
        //db.Database.EnsureCreated();
        db.Database.Migrate();

        return db;
    }
    public static void Data<T>(this MainDB db, params T[] list) where T : class => db.Data(list);
    public static MainDB Data<T>(this MainDB db, IEnumerable<T> list) where T : class {
        var set = db.Set<T>();
        if (!set.Any()) {
            set.AddRange(list);
            db.SaveChanges();
        }
        return db;
    }
}
