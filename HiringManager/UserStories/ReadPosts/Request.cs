﻿using NSubstitute;

namespace Blogger.UserStories.ReadPosts;

public record Request(
    string Title,
    string Content) : Core.UserStory.RequestCore
{
    public class MockBuilder
    {
        public readonly Request Mock = Substitute.For<Request>("Title", "Content");

        public MockBuilder() => UseValidRequest();

        public MockBuilder UseValidRequest()
        {
            //Mock.Title.Returns("Title");
            //Mock.Content.Returns("Content");
            return this;
        }

        public MockBuilder UseInvalidRequest()
        {
            UseValidRequest();
            Mock.Title.Returns((string)null);
            return this;
        }

    }

}