﻿using Core.Enterprise;
using Core.Enterprise.UserStory;
using Microsoft.Extensions.DependencyInjection;

namespace Design.Core.Enterprise;

public class Extensions_Design
{
    [Fact]
    public void AddUserStory_Registers_All_UserStory()
    {
        var services = new ServiceCollection();

        services.AddCore();

        var sp = services.BuildServiceProvider();
        var userStory = sp.GetRequiredService<IUserStory<RequestCore, ResponseCore<RequestCore>>>();
        userStory.Should().NotBeNull();
    }
}