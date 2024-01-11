﻿using AppPolicy;
using AppPolicy.UserStory;
using AppPolicy.UserStory.DomainModel;
using Microsoft.Extensions.DependencyInjection;

namespace Design.AppPolicy;

public class Extensions_Design
{
    [Fact]
    public void AddUserStory_Registers_All_UserStory()
    {
        var services = new ServiceCollection();

        services.AddCoreSystem();

        var sp = services.BuildServiceProvider();
        var userStory = sp.GetRequiredService<IUserStory<Request, Response<Request>>>();
        userStory.Should().NotBeNull();
    }
}
