﻿using Microsoft.Extensions.DependencyInjection;

namespace Core.Enterprise.UserStory;

public static class Extensions
{
    public static IServiceCollection AddUserStory(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUserStory<,>), typeof(UserStoryCore<,>));

        return services;
    }
}
