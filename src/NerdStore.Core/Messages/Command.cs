using FluentValidation.Results;
using MediatR;
using System;

namespace NerdStore.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool Valido()
        {
            throw new NotImplementedException();
        }
    }
}
