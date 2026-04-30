using System;

namespace mss.ims.domain.Common
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
    }
}
