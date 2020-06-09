namespace Severino.Domain.Shared
{
    using System;

    using static Severino.Infrastructure.Clock;

    public abstract class Entity
    {
        protected Entity() => this.CreationAt = UtcNow();

        public DateTime CreationAt { get; }
    }
}
