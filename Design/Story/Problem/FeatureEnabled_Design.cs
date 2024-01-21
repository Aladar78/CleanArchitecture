﻿using Common;
using Common.Model;
using Common.Problems;
using Microsoft.Extensions.DependencyInjection;

namespace Story.Problem;

public class FeatureEnabled_Design
{
    [Fact]
    public async void FeatureFlagIsFalse()
    {
        var response = new Response<Request>();
        var token = CancellationToken.None;
        var unit = new FeatureEnabled<Request, Response<Request>>();

        await unit.Run(response, token);

        response.FeatureEnabled.Should().BeFalse();
        response.Terminated.Should().BeTrue();
    }

    [Fact]
    public void AddFeatureTask_Registers_All_UserTask()
    {
        var services = new ServiceCollection();

        services.AddStory();

        var sp = services.BuildServiceProvider();
        var problem = sp.GetRequiredService<FeatureEnabled<Request, Response<Request>>>();
        problem.Should().NotBeNull();
    }
}