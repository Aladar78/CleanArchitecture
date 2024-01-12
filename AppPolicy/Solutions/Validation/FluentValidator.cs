﻿using Core;
using FluentValidation;
using FluentValidation.Results;
using Core.Sockets.ValidationModel;

namespace Core.Solutions.Validation;

public abstract class FluentValidator<T> : AbstractValidator<T> {
    public async Task<IEnumerable<ValidationSolutionExpertModel>> Validate(T request, CancellationToken token) {
        var pluginModel = await ValidateAsync(request, token);
        var socketModel = pluginModel.Errors.Select(ToSocketModel);
        return socketModel;
    }

    private ValidationSolutionExpertModel ToSocketModel(ValidationFailure plugin) => new(
        plugin.PropertyName,
        plugin.ErrorCode,
        plugin.ErrorMessage,
        plugin.Severity.ToString());
}


