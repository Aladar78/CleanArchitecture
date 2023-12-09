﻿using BloggerUserRole.ReadPostsFaeture.UserStoryLayer.UserStoryUnit;
using Core.UserStoryLayer.UserStoryUnit;
using Principals.UserStoryLayer.UserStoryUnit;
using Sys.UserStory.UserStoryUnit;

namespace Spec.Blogger_Specification.ReadPosts.BusinessWorkFlow;

public class Feature_Specification
{
    //[Fact]
    public async void NonStoppedFeature()
    {
        workSteps.UseNonStoppedWorkSteps();

        var unit = new UserStory(workSteps.Mock);
        var response = await unit.Run(feature.Request, feature.Token);

        response.Should().NotBeNull();
        response.Request.Should().Be(feature.Request);
        response.Stopped.Should().BeFalse();
        response.Posts.Should().BeNull();
        response.Validations.Should().BeNull();
    }

    //[Fact]
    public async void StoppedFeature()
    {
        workSteps.MockStoppedWorkSteps();

        var unit = new UserStory(workSteps.Mock);
        var response = await unit.Run(feature.Request, feature.Token);

        response.Should().NotBeNull();
        response.Request.Should().Be(feature.Request);
        response.Stopped.Should().BeTrue();
    }

    private readonly WorkStep_MockBuilder workSteps = new();
    private readonly Featrue_MockBuilder feature = new();
}

public class Featrue_MockBuilder
{
    public readonly IUserStory<BloggerUserRole.ReadPostsFaeture.UserStoryLayer.UserStoryUnit.Request, Response> Mock = Substitute.For<IUserStory<BloggerUserRole.ReadPostsFaeture.UserStoryLayer.UserStoryUnit.Request, Response>>();
    public BloggerUserRole.ReadPostsFaeture.UserStoryLayer.UserStoryUnit.Request Request;
    public CancellationToken Token;

    public Featrue_MockBuilder() => UseValidRequest().UseNoneCanceledToken();

    public Featrue_MockBuilder UseValidRequest()
    {
        Request = new Blogger.ReadPosts.UserStory.Request("Title", "Content");
        Request = Request with { Title = Request.Title, Content = Request.Content };
        return this;
    }

    public Featrue_MockBuilder UseInvalidRequest()
    {
        Request = new Blogger.ReadPosts.UserStory.Request(null, null);
        Request = Request with { Title = Request.Title, Content = Request.Content };
        return this;
    }

    public Featrue_MockBuilder UseNoneCanceledToken()
    {
        Token = CancellationToken.None;
        return this;
    }
}

public class WorkStep_MockBuilder
{
    public readonly List<ITask<Response>> Mock = new List<ITask<Response>>();

    public WorkStep_MockBuilder() => UseNonStoppedWorkSteps();

    public WorkStep_MockBuilder UseNonStoppedWorkSteps()
    {
        Mock.Clear();
        Mock.Add(new ContinueWorkStep());
        Mock.Add(new ContinueWorkStep());
        Mock.Add(new ContinueWorkStep());
        return this;
    }

    public WorkStep_MockBuilder MockStoppedWorkSteps()
    {
        Mock.Clear();
        Mock.Add(new ContinueWorkStep());
        Mock.Add(new StopWorkStep());
        Mock.Add(new ContinueWorkStep());
        return this;
    }

    public class StopWorkStep : ITask<Response>
    {
        public Task Run(Response response, CancellationToken cancellation)
        {
            response.Stopped = true;
            return Task.CompletedTask;
        }
    }

    public class ContinueWorkStep : ITask<Response>
    {
        public Task Run(Response response, CancellationToken cancellation)
        {
            response.Stopped = false;
            return Task.CompletedTask;
        }
    }
}