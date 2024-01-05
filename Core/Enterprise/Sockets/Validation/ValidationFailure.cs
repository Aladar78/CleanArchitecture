﻿namespace Core.Enterprise.Sockets.Validation;

public sealed record ValidationFailure(
    string PropertyName,
    string ErrorCode,
    string ErrorMessage,
    string Severity);