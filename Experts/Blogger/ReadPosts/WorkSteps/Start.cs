﻿using Core;
using Core.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Experts.Blogger.ReadPosts.WorkSteps;

public class Start(ITime time) : UserWorkStep<UserStoryRequest, UserStoryResponse> {
    public override Task<bool> Run(UserStoryResponse response, CancellationToken token) {
        response.MetaData.StartedAt = time.Now;
        return true.ToTask();
    }
}

public static class StartUserWorkStepExtensions {
    public static IServiceCollection AddStartUserWorkStep(this IServiceCollection services) => services
        .AddScoped<IUserWorkStep<UserStoryRequest, UserStoryResponse>, Start>();
}



