﻿namespace Core.UserStory.UserStoryUnit;

public interface ITask<TRequest, TResponse>
    where TRequest : RequestCore
    where TResponse : ResponseCore<TRequest>, new()
{
    Task Run(TResponse response, CancellationToken cancellation);
}