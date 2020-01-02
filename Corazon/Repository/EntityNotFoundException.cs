using System;

namespace Corazon
{
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException(Identity id)
            : base($"The '{typeof(T).Name}' with Id '{id}' was not found.")
        {
        }
    }
}