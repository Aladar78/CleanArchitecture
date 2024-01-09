﻿using Core.Sys.UserStory.DomainModel;

namespace Design.Core.Sys.UserStory;

public class RequestCore_Design
{
    [Fact]
    public void Test_RequestCore()
    {
        var request = new Request();

        request = request with { };

        request.Should().NotBeNull();
        request.Should().BeOfType<Request>();
    }
}