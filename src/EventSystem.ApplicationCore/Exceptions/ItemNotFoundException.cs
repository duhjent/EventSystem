using System;

namespace EventSystem.ApplicationCore.Exceptions
{
    public class ItemNotFoundException<T> : Exception where T : class
    {
        public ItemNotFoundException(string message) : base(message) { }
    }
}
