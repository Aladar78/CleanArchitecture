﻿using NSubstitute;

namespace Core.UserStory;

public interface ITask<TRequest, TResponse>
    where TRequest : RequestCore
    where TResponse : ResponseCore<TRequest>, new()
{
    Task Run(TResponse response, CancellationToken token);

    
    public class CanContinueTask : ITask<RequestCore, ResponseCore<RequestCore>>
    {
        public async Task Run(ResponseCore<RequestCore> response, CancellationToken token) => response.CanRun = await true.ToTask();
    }

    public class CanNotContinueTask : ITask<RequestCore, ResponseCore<RequestCore>>
    {
        public async Task Run(ResponseCore<RequestCore> response, CancellationToken token) => response.CanRun = await false.ToTask();
    }
    public class MockBuilder
    {
        public ITask<RequestCore, ResponseCore<RequestCore>> Mock = Substitute.For<ITask<RequestCore, ResponseCore<RequestCore>>>();

        public MockBuilder Stopped()
        {
            Mock
                .WhenForAnyArgs(x => x.Run(default, default))
                .Do(x =>
                {
                    var response = x.ArgAt<TResponse>(0);
                    response.CanRun = false;
                });
            return this;
        }

        public MockBuilder NonStopped()
        {
            Mock
                .WhenForAnyArgs(x => x.Run(default, default))
                .Do(x =>
                {
                    var response = x.ArgAt<TResponse>(0);
                    response.CanRun = true;
                });
            return this;
        }
    }
}
