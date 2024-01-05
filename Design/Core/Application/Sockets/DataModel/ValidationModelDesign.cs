﻿namespace Design.Core.Application.Sockets.DataModel;

public class ValidationModelDesign
{
    [Fact]
    public void Ctor()
    {
        var propertyName = "propertyName";
        var errorCode = "errorCode";
        var errorMessage = "errorMessage";
        var severity = "severity";
        var result = new ValidationFailure(propertyName, errorCode, errorMessage, severity);
        result = result with { PropertyName = propertyName, ErrorCode = errorCode, ErrorMessage = errorMessage, Severity = severity };

        result.Should().NotBeNull();
        result.PropertyName.Should().NotBeNull();
        result.ErrorMessage.Should().NotBeNull();
        result.Severity.Should().NotBeNull();
    }
}