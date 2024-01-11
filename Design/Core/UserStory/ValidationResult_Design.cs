﻿using Core.UserStory.DomainModel;

namespace Design.Core.UserStory;

public class ValidationResult_Design
{
    [Fact]
    public void ValidationResult_Success()
    {
        var result = ValidationDomainModel.Success();

        result.Should().NotBeNull();
        result.ErrorCode.Should().BeNull();
        result.ErrorMessage.Should().BeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void ValidationResult_Failed()
    {
        var result = ValidationDomainModel.Failed("ErrorCode", "ErrorMessage");

        result.Should().NotBeNull();
        result.ErrorCode.Should().NotBeNull();
        result.ErrorMessage.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
    }
}