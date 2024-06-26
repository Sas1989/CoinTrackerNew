﻿namespace Common.Domain;

public record Result
{
    protected Result(bool isSuccess, Error error)
    {
        if(isSuccess && error != Error.None) {
            throw new ArgumentException("In case of success Error should be empty");
        }

        if(!isSuccess && error == Error.None) {
            throw new ArgumentException("In case of failure Error cannot be empty");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static implicit operator Result(Error error) => Failure(error);

}
