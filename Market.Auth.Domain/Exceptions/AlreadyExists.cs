﻿namespace Market.Auth.Domain.Exceptions;

public class AlreadyExists : Exception
{
    public AlreadyExists(string message) : base(message) { }
}
