﻿namespace InsightLog.Domain.Common;
public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}