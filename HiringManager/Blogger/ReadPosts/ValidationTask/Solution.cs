﻿using Common.Solutions.DataModel.ValidationModel;
using FluentValidation;
using FluentValidation.Results;

namespace Experts.Blogger.ReadPosts.ValidationTask;

public class Solution : AbstractValidator<Request>, ISolution {
    public Solution() {
        RuleFor(request => request.Title)
            .NotEmpty().When(request => string.IsNullOrWhiteSpace(request.Content), ApplyConditionTo.CurrentValidator)
            .WithMessage(request => $"'{nameof(request.Title)}' can not be empty if '{nameof(request.Content)}' is empty.")
            .MinimumLength(3).When(request => !string.IsNullOrWhiteSpace(request.Title), ApplyConditionTo.CurrentValidator);

        RuleFor(request => request.Content)
            .NotEmpty().When(request => string.IsNullOrWhiteSpace(request.Title), ApplyConditionTo.CurrentValidator)
            .WithMessage(request => $"'{nameof(request.Content)}' can not be empty if '{nameof(request.Title)}' is empty.")
            .MinimumLength(3).When(request => !string.IsNullOrWhiteSpace(request.Content), ApplyConditionTo.CurrentValidator);
    }

    public async Task<IEnumerable<ValidationIssue>> Validate(Request request, CancellationToken token) {
        var technologyModel = await ValidateAsync(request, token);
        var solutionModel = technologyModel.Errors.Select(ToSolutionModel);
        return solutionModel;
    }

    private ValidationIssue ToSolutionModel(ValidationFailure technologyModel) => new(
        technologyModel.PropertyName,
        technologyModel.ErrorCode,
        technologyModel.ErrorMessage,
        technologyModel.Severity.ToString());
}
