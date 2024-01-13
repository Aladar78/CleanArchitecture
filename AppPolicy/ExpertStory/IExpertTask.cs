﻿using Core.ExpertStory.StoryModel;

namespace Core.ExpertStory;

public interface IExpertTask<TRequest, TResponse>
    where TRequest : Request
    where TResponse : Response<TRequest>, new() {
    Task Run(TResponse response, CancellationToken token);
}