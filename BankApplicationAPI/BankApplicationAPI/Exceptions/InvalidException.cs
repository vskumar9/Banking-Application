﻿namespace BankApplicationAPI.Exceptions
{
    public class InvalidException : Exception
    {
        public InvalidException(string message) : base(message) { }
    }
}
