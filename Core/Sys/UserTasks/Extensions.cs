﻿using Core.Sys.UserStory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Sys.UserTasks;

public static class Extensions
{
    public static IServiceCollection AddFeatureTask(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUserTask<,>), typeof(SrartTask<,>));
        services.AddScoped(typeof(IUserTask<,>), typeof(FeatureTask<,>));

        services.AddScoped(typeof(IUserTask<,>), typeof(EndTask<,>));

        return services;
    }
}